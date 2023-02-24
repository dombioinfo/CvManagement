using Microsoft.AspNetCore.Components;

namespace BlazorBase.Shared.Component
{
    public partial class AdressesModal : ComponentBase
    {
        [Parameter]
        public long PersonneId { get; set; }

        public string Title { get; set; } = "Edition d'une adresse";
        private Modal? modalAdresseRef;

        protected override async Task OnInitializedAsync()
        {
            // this.ShowModal();
            await base.OnInitializedAsync();
        }

        private Task ShowModal()
        {
            return modalAdresseRef?.Show();
        }

        private Task HideModal()
        {
            return modalAdresseRef?.Hide();
        }
    }
}
