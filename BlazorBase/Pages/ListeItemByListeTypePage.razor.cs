using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using Blazorise.DataGrid;
using System.Drawing;
using BlazorBase.Shared.Component;

namespace BlazorBase.Pages
{
    public partial class ListeItemByListeTypePage : ComponentBase
    {
        private string PageTitle { get; set; } = "";

        [Inject]
        private ListeTypeService ListeTypeService { get; set; } = default!;
        [Inject]
        protected ListeItemService ListeItemService { get; set; } = default!;
        [Inject]
        public IModalService ModalService { get; set; } = default!;
        [Inject]
        public IMessageService MessageService { get; set; } = default!;
        [Parameter]
        public long ListeTypeId { get; set; }
        public ListeTypeDto ListeTypeDto { get; set; }
        private IEnumerable<ListeItemDto> Items { get; set; } = default!;
        private ListeItemDto? SelectedRow;
        private int TotalItems;
        bool ShowContextMenu = false;
        ListeItemDto? ContextMenuListeItemDto;
        Point ContextMenuPos;

        protected override async Task OnInitializedAsync()
        {
            ListeTypeDto = await ListeTypeService.GetListeTypeAsync(ListeTypeId);
            Items = await ListeItemService.GetListeItemsByListeTypeAsync(ListeTypeId);

            await base.OnInitializedAsync();
        }

        private async Task OnReadData(DataGridReadDataEventArgs<ListeItemDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                List<ListeItemDto> response = default!;

                // this can be call to anything, in this case we're calling a fictional api
                //var response = await Http.GetJsonAsync<Employee[]>( $"some-api/employees?page={e.Page}&pageSize={e.PageSize}" );
                if (e.ReadDataMode is DataGridReadDataMode.Virtualize)
                    response = (await ListeItemService.GetListeItemsAsync()).Skip(e.VirtualizeOffset).Take(e.VirtualizeCount).ToList();
                else if (e.ReadDataMode is DataGridReadDataMode.Paging)
                    response = (await ListeItemService.GetListeItemsAsync()).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
                else
                    throw new Exception("Unhandled ReadDataMode");

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    TotalItems = (await ListeItemService.GetListeItemsAsync()).Count();
                    Items = new List<ListeItemDto>(response); // an actual data for the current page
                }
            }
        }

        private async void OnNewItemDefaultSetter(ListeItemDto listeItemDto)
        {
            listeItemDto.DateCreation = DateTime.UtcNow;
            listeItemDto.ListeTypeDtoId = ListeTypeId;
            StateHasChanged();
        }

        private async Task<int> OnRowInserted(SavedRowItem<ListeItemDto, Dictionary<string, object>> e)
        {
            int listeTypeId = await ListeItemService.CreateListeItemAsync(e.OldItem);
            StateHasChanged();
            return listeTypeId;
        }

        private async Task OnRowUpdated(SavedRowItem<ListeItemDto, Dictionary<string, object>> e)
        {
            e.Item.ListeTypeDtoId = ListeTypeId;
            int id = await ListeItemService.UpdateListeItemAsync(e.Item.Id, e.Item);
        }

        public async Task OnRowRemoving(CancellableRowChange<ListeItemDto> e)
        {
            e.Cancel = await ShowDeleteConfirmMessage();
        }

        public async Task OnRowRemoved(ListeItemDto listeItemDto)
        {
            await ListeItemService.DeleteListeItemAsync(listeItemDto.Id);
        }

        protected Task OnRowContextMenu(DataGridRowMouseEventArgs<ListeItemDto> eventArgs)
        {
            ShowContextMenu = true;
            ContextMenuListeItemDto = eventArgs.Item;
            ContextMenuPos = eventArgs.MouseEventArgs.Client;

            return Task.CompletedTask;
        }

        protected Task? OnContextAdresseEditClicked(ListeChoixDto listeType)
        {
            ShowContextMenu = false;
            return ModalService?.Show<ListeChoixModal>(parameters => parameters.Add(x => x.EnteteId, listeType.Id), new ModalInstanceOptions() { UseModalStructure = false });
        }

        protected Task? OnContextCandidatureEditClicked(ListeChoixDto listeChoix)
        {
            ShowContextMenu = false;
            return null;
        }

        public async Task<bool> ShowDeleteConfirmMessage()
        {
            var confirmed = await MessageService.Confirm("Etes-vous de vouloir supprimer ?", "Confirmation");
            if (confirmed)
            {
                return false; // pas d'annulation
            }
            return true; // annulation
        }
    }
}
