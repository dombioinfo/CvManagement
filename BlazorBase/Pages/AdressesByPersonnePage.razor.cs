using BlazorBase.Service;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Pages {
    public partial class AdressesByPersonnePage : ComponentBase
    {
        [Inject]
        private PersonneService PersonneService { get; set; } = default!;
        [Inject]
        private AdresseService AdresseService { get; set; } = default!;
        [Parameter]
        public long PersonneId { get; set; }
        private PersonneDto Personne { get; set; } = default!;

        private IEnumerable<AdresseDto> Items { get; set; } = default!;
        private AdresseDto? SelectedAdresse;
        private int TotalItems { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            Personne = await PersonneService.GetPersonneAsync(PersonneId);
            Items = await AdresseService.GetAdressesByPersonneAsync(PersonneId);
            await base.OnInitializedAsync();
        }
        private async Task OnReadData(DataGridReadDataEventArgs<AdresseDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                List<AdresseDto> response = default!;

                // this can be call to anything, in this case we're calling a fictional api
                //var response = await Http.GetJsonAsync<Employee[]>( $"some-api/employees?page={e.Page}&pageSize={e.PageSize}" );
                if (e.ReadDataMode is DataGridReadDataMode.Virtualize)
                    response = (await AdresseService.GetAdressesAsync()).Skip(e.VirtualizeOffset).Take(e.VirtualizeCount).ToList();
                else if (e.ReadDataMode is DataGridReadDataMode.Paging)
                    response = (await AdresseService.GetAdressesAsync()).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
                else
                    throw new Exception("Unhandled ReadDataMode");

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    TotalItems = (await AdresseService.GetAdressesAsync()).Count();
                    Items = new List<AdresseDto>(response); // an actual data for the current page
                }
            }
        }

        private void OnNewItemDefaultSetter(AdresseDto adresseDto) {
            adresseDto.DateCreation = DateTime.UtcNow;
            StateHasChanged();
        }

        private async Task<long> OnRowInserted(SavedRowItem<AdresseDto, Dictionary<string, object>> e) {
            long id = await AdresseService.CreateAdresseAsync(e.Item);
            StateHasChanged();
            return id;
        }

        private async Task OnRowUpdated(SavedRowItem<AdresseDto, Dictionary<string, object>> e) {
            int id = await AdresseService.UpdateAdresseAsync(e.Item.Id, e.Item);
        }

        public async Task OnRowRemoved(AdresseDto adresseDto) {
            await AdresseService.DeleteAdresseAsync(adresseDto.Id);
        }
        
    }
}
