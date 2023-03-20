using System.Text.Json.Serialization;

namespace BlazorBaseModel.Model
{
    public class AdresseDto : GenericObject
    {
        public AdresseDto() : base() { }

        public string Rue { get; set; } = "";
        public string Complement { get; set; } = "";
        public string Ville { get; set; } = "";
        public string CodePostal { get; set; } = default!;
        public long PersonneId { get; set; } = default!;

        [JsonIgnore]
        public virtual PersonneDto PersonneDto { get; set; } = default!;
    }
}
