using BlazorBase.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Shared.Component
{
    public partial class ListeChoixForm : ComponentBase
    {
        private readonly ListeChoixService _listeChoixService;

        public ListeChoixForm(ListeChoixService listeChoixService)
        {
            _listeChoixService = listeChoixService;
        }

        //[Parameter]
        public long SelectedValueForListeType { get; set; } = default;
        //[Parameter]
        public long SelectedValueForListeItem { get; set; } = default;
        [Parameter]
        public ListeChoixDto CurrentListeChoixDto
        {
            get
            {
                if (ListeChoixListe != null) {
                    return ListeChoixListe.First(o => o.Id == SelectedValueForListeType);
                } else {
                    return null;
                }
            }
            set { }
        }

        List<ListeChoixDto> ListeChoixListe = new();
        public List<ListeTypeDto> ListeCategorieListe {
            get
            {
                List<ListeTypeDto> listeTypeListe = new List<ListeTypeDto>();
                ListeChoixListe.ForEach(e => listeTypeListe.Add(e.Entete));
                return listeTypeListe;
            }
        }
        public List<ListeItemDto> ListeItemListe
        {
            get
            {
                return CurrentListeChoixDto.ItemList.Values.ToList();
            }
            set
            {

            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ListeChoixListe = await _listeChoixService.GetListeListeChoixAsync();
        }

        public async Task OnCategorieClickedToAdd()
        {

        }

        private async Task OnCategorieClieckedToUpdate()
        {

        }
    }
}