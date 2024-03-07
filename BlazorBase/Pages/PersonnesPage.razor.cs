using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using BlazorBaseModel.Model;
using Blazorise.DataGrid;
using System.Drawing;
using BlazorBase.Shared.Component;
using Microsoft.JSInterop;

namespace BlazorBase.Pages
{
    public partial class PersonnesPage : ComponentBase
    {
        private string PageTitle { get; set; } = "";

        [Inject]
        protected PersonneService PersonneService { get; set; } = default!;
        [Inject]
        private ListeItemService ListeItemService { get; set; } = default!;
        [Inject]
        public IModalService ModalService { get; set; } = default!;
        [Inject]
        public IMessageService MessageService { get; set; } = default!;
        private IEnumerable<PersonneDto> Items { get; set; } = default!;
        private PersonneDto? SelectedRow;
        private int TotalItems;
        public List<ListeItemDto> Civilites { get; set; } = default!;
        public long SelectedCiviliteListValue { get; set; } = default!;
        bool ShowContextMenu = false;
        PersonneDto? ContextMenuPersonne;
        Point ContextMenuPos;

        protected override async Task OnInitializedAsync()
        {
            Civilites = await ListeItemService.GetListeItemsByListeTypeAsync("CIVILITE");
            Items = await PersonneService.GetPersonnesAsync();

            await base.OnInitializedAsync();
        }

        private async Task OnReadData(DataGridReadDataEventArgs<PersonneDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                List<PersonneDto> response = default!;

                // this can be call to anything, in this case we're calling a fictional api
                //var response = await Http.GetJsonAsync<Employee[]>( $"some-api/employees?page={e.Page}&pageSize={e.PageSize}" );
                if (e.ReadDataMode is DataGridReadDataMode.Virtualize)
                    response = (await PersonneService.GetPersonnesAsync()).Skip(e.VirtualizeOffset).Take(e.VirtualizeCount).ToList();
                else if (e.ReadDataMode is DataGridReadDataMode.Paging)
                    response = (await PersonneService.GetPersonnesAsync()).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
                else
                    throw new Exception("Unhandled ReadDataMode");

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    TotalItems = (await PersonneService.GetPersonnesAsync()).Count();
                    Items = new List<PersonneDto>(response); // an actual data for the current page
                }
            }
        }

        private void OnNewItemDefaultSetter(PersonneDto personneDto)
        {
            personneDto.DateCreation = DateTime.UtcNow;
            StateHasChanged();
        }

        private async Task<int> OnRowInserted(SavedRowItem<PersonneDto, Dictionary<string, object>> e)
        {
            int personneId = await PersonneService.CreatePersonneAsync(e.NewItem);
            e.NewItem.Id = personneId;
            StateHasChanged();
        
            return personneId;
        }

        private async Task OnRowUpdated(SavedRowItem<PersonneDto, Dictionary<string, object>> e)
        {
            int id = await PersonneService.UpdatePersonneAsync(e.NewItem.Id, e.NewItem);
        }

        public async Task OnRowRemoving(CancellableRowChange<PersonneDto> e)
        {
            e.Cancel = await ShowDeleteConfirmMessage();
        }

        public async Task OnRowRemoved(PersonneDto personneDto)
        {
            await PersonneService.DeletePersonneAsync(personneDto.Id);
        }

        protected Task OnRowContextMenu(DataGridRowMouseEventArgs<PersonneDto> eventArgs)
        {
            ShowContextMenu = true;
            ContextMenuPersonne = eventArgs.Item;
            ContextMenuPos = eventArgs.MouseEventArgs.Client;

            return Task.CompletedTask;
        }

        protected Task? OnContextAdresseEditClicked(PersonneDto personne)
        {
            ShowContextMenu = false;
            return ModalService?.Show<AdressesModal>(parameters => parameters.Add(x => x.PersonneId, personne.Id), new ModalInstanceOptions() { UseModalStructure = false });
        }

        protected Task? OnContextCandidatureEditClicked(PersonneDto personne)
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
