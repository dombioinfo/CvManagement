using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using Blazorise.DataGrid;
using System.Drawing;
using BlazorBase.Shared.Component;

namespace BlazorBase.Pages
{
    public partial class ListeTypePage : ComponentBase
    {
        private string PageTitle { get; set; } = "";

        [Inject]
        protected ListeTypeService ListeTypeService { get; set; } = default!;
        [Inject]
        public IModalService ModalService { get; set; } = default!;
        [Inject]
        public IMessageService MessageService { get; set; } = default!;
        private IEnumerable<ListeTypeDto> Items { get; set; } = default!;
        private ListeTypeDto? SelectedRow;
        private int TotalItems;
        bool ShowContextMenu = false;
        ListeTypeDto? ContextMenuListeTypeDto;
        Point ContextMenuPos;

        protected override async Task OnInitializedAsync()
        {
            Items = await ListeTypeService.GetListeTypesAsync();
            await base.OnInitializedAsync();
        }

        private async Task OnReadData(DataGridReadDataEventArgs<ListeTypeDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                List<ListeTypeDto> response = default!;

                // this can be call to anything, in this case we're calling a fictional api
                //var response = await Http.GetJsonAsync<Employee[]>( $"some-api/employees?page={e.Page}&pageSize={e.PageSize}" );
                if (e.ReadDataMode is DataGridReadDataMode.Virtualize)
                    response = (await ListeTypeService.GetListeTypesAsync()).Skip(e.VirtualizeOffset).Take(e.VirtualizeCount).ToList();
                else if (e.ReadDataMode is DataGridReadDataMode.Paging)
                    response = (await ListeTypeService.GetListeTypesAsync()).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
                else
                    throw new Exception("Unhandled ReadDataMode");

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    TotalItems = (await ListeTypeService.GetListeTypesAsync()).Count();
                    Items = new List<ListeTypeDto>(response); // an actual data for the current page
                }
            }
        }

        private void OnNewItemDefaultSetter(ListeTypeDto listeTypeDto)
        {
            listeTypeDto.DateCreation = DateTime.UtcNow;
            StateHasChanged();
        }

        private async Task<int> OnRowInserted(SavedRowItem<ListeTypeDto, Dictionary<string, object>> e)
        {
            int listeTypeId = await ListeTypeService.CreateListeTypeAsync(e.Item);
            StateHasChanged();
            return listeTypeId;
        }

        private async Task OnRowUpdated(SavedRowItem<ListeTypeDto, Dictionary<string, object>> e)
        {
            //int id = await ListeTypeService.UpdateListeTypeAsync(e.Item.Id, e.Item);
        }

        public async Task OnRowRemoving(CancellableRowChange<ListeTypeDto> e)
        {
            e.Cancel = await ShowDeleteConfirmMessage();
        }

        public async Task OnRowRemoved(ListeTypeDto listeTypeDto)
        {
            await ListeTypeService.DeleteListeTypeAsync(listeTypeDto.Id);
        }

        protected Task OnRowContextMenu(DataGridRowMouseEventArgs<ListeTypeDto> eventArgs)
        {
            ShowContextMenu = true;
            ContextMenuListeTypeDto = eventArgs.Item;
            ContextMenuPos = eventArgs.MouseEventArgs.Client;

            return Task.CompletedTask;
        }

        protected Task? OnContextAdresseEditClicked(ListeTypeDto listeType)
        {
            ShowContextMenu = false;
            return ModalService?.Show<ListeChoixModal>(parameters => parameters.Add(x => x.EnteteId, listeType.Id), new ModalInstanceOptions() { UseModalStructure = false });
        }

        protected Task? OnContextCandidatureEditClicked(ListeTypeDto listeChoix)
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
