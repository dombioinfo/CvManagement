using Microsoft.AspNetCore.Components;

namespace BlazorBase.Shared.Component
{
    public partial class ListeChoixModal : ComponentBase
    {
        [Parameter]
        public long EnteteId { get; set; }

        public string Title { get; set; } = "Edition d'une liste de choix";
        private Modal? modalListeChoixRef;

        protected override async Task OnInitializedAsync()
        {
            // this.ShowModal();
            await base.OnInitializedAsync();
        }

        private Task ShowModal()
        {
            return modalListeChoixRef?.Show();
        }

        private Task HideModal()
        {
            return modalListeChoixRef?.Hide();
        }
    }
}
