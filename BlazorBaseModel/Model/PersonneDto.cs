using System.Text.Json.Serialization;

namespace BlazorBaseModel.Model
{
    public class PersonneDto : GenericObject
    {
        public PersonneDto() : base() { }

        public string Nom { get; set; } = "";

        public string Prenom { get; set; } = "";

        public DateTime? DateNaissance { get; set; } = default!;

        public string? Email { get; set; } = default!;

        public string? Tel { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<AdresseDto> AdresseDtos { get; set; } = default!;
    }
}
