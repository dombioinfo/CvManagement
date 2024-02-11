using BlazorBase.Service;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Pages
{
    public partial class CandidaturesByPersonnePage : ComponentBase
    {
        [Inject]
        private PersonneService PersonneService { get; set; } = default!;
        [Inject]
        private ListeItemService ListeItemService { get; set; } = default!;
        [Inject]
        private CandidatureService CandidatureService { get; set; } = default!;
        [Inject]
        public IModalService ModalService { get; set; } = default!;
        [Inject]
        public IMessageService MessageService { get; set; } = default!;
        [Parameter]
        public long PersonneId { get; set; }
        private PersonneDto Personne { get; set; } = default!;

        private IEnumerable<CandidatureDto> Items { get; set; } = default!;
        private CandidatureDto? SelectedItem;
        private int TotalItems { get; set; } = 0;

        public List<ListeItemDto> Metiers { get; set; } = default!;
        public long SelectedMetierListValue { get; set; } = default!;

        public List<ListeItemDto> Statuts { get; set; } = default!;
        public long SelectedStatutListValue { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Personne = await PersonneService.GetPersonneAsync(PersonneId);
            Metiers = await ListeItemService.GetListeItemsByListeTypeAsync("METIER");
            Statuts = await ListeItemService.GetListeItemsByListeTypeAsync("CANDIDATURE_STATUT");
            Items = await CandidatureService.GetCandidaturesByPersonneAsync(PersonneId);
            foreach (CandidatureDto candidatureDto in Items)
            {
                candidatureDto.Metier = Metiers.FirstOrDefault(x => x.Id == candidatureDto.MetierId);
                candidatureDto.Statut = Statuts.FirstOrDefault(x => x.Id == candidatureDto.StatutId);
                candidatureDto.Personne = Personne;
            }
            await base.OnInitializedAsync();
        }
        
        private async Task OnReadData(DataGridReadDataEventArgs<CandidatureDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                List<CandidatureDto> response = default!;

                // this can be call to anything, in this case we're calling a fictional api
                //var response = await Http.GetJsonAsync<Employee[]>( $"some-api/employees?page={e.Page}&pageSize={e.PageSize}" );
                if (e.ReadDataMode is DataGridReadDataMode.Virtualize)
                    response = (await CandidatureService.GetCandidaturesAsync()).Skip(e.VirtualizeOffset).Take(e.VirtualizeCount).ToList();
                else if (e.ReadDataMode is DataGridReadDataMode.Paging)
                    response = (await CandidatureService.GetCandidaturesAsync()).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
                else
                    throw new Exception("Unhandled ReadDataMode");

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    TotalItems = (await CandidatureService.GetCandidaturesAsync()).Count();
                    Items = new List<CandidatureDto>(response); // an actual data for the current page
                }
            }
        }

        private void OnNewItemDefaultSetter(CandidatureDto candidatureDto)
        {
            candidatureDto.DateCreation = DateTime.UtcNow;
            candidatureDto.DateCandidature = DateTime.UtcNow;
            candidatureDto.PersonneId = PersonneId;
            StateHasChanged();
        }

        private async Task<long> OnRowInserted(SavedRowItem<CandidatureDto, Dictionary<string, object>> e)
        {
            long id = await CandidatureService.CreateCandidatureAsync(e.Item);
            StateHasChanged();
            return id;
        }

        private async Task OnRowUpdated(SavedRowItem<CandidatureDto, Dictionary<string, object>> e)
        {
            int id = await CandidatureService.UpdateCandidatureAsync(e.Item.Id, e.Item);
        }

        public async Task OnRowRemoved(CandidatureDto candidatureDto)
        {
            await CandidatureService.DeleteCandidatureAsync(candidatureDto.Id);
        }

        public async Task OnRowRemoving(CancellableRowChange<CandidatureDto> e)
        {
            e.Cancel = await ShowDeleteConfirmMessage();
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
