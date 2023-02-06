using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using BlazorBaseModel.Model;
using Blazorise.DataGrid;

namespace BlazorBase.Pages
{
    public partial class PersonnesPage : ComponentBase
    {
        private string PageTitle { get; set; } = "";

        [Inject]
        protected PersonneService PersonneService { get; set; } = default!;
        private IEnumerable<PersonneDto> personnes { get; set; } = default!;
        private PersonneDto? selectedPersonne;
        private int totalPersonnes;

        protected override async Task OnInitializedAsync()
        {
            personnes = await PersonneService.GetPersonnesAsync();
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
                    totalPersonnes = (await PersonneService.GetPersonnesAsync()).Count();
                    personnes = new List<PersonneDto>(response); // an actual data for the current page
                }
            }
        }
    }
}
