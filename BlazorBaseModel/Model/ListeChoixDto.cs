using System.Text.Json.Serialization;
using BlazorBaseModel.Db;

namespace BlazorBaseModel.Model
{
    public class ListeChoixDto : GenericObject
    {
        public ListeChoixDto() : base() { }

        public ListeTypeDto Entete { get; set; } = default!;

        public Dictionary<string, ListeItemDto> ItemList { get; set; } = new Dictionary<string, ListeItemDto>();
    }
}
