using System.Text.Json.Serialization;

namespace BlazorBaseModel.Model
{
    public class ListeTypeDto : GenericObject
    {
        public ListeTypeDto() : base() { }

        public string Code { get; set; } = "";
        public string DefaultLibelle { get; set; } = "";
        [JsonIgnore]
        public virtual ICollection<ListeItemDto> ListeItemDtos { get; set; } = default!;
    }
}