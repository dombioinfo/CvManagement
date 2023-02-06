using BlazorBase.Service;
using Microsoft.AspNetCore.Components;
using BlazorBaseModel.Model;

namespace BlazorBase.Pages
{
    public partial class PersonnesPage : ComponentBase
    {
        private string PageTitle { get; set; } = "";

        [Inject]
        protected PersonneService PersonneService { get; set; } = default!;
        private IEnumerable<PersonneDto> personnes { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            personnes = await PersonneService.GetPersonnesAsync();
        }
    }
}

