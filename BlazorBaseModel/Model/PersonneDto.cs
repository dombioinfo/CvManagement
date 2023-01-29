using BlazorBaseModel.Db;

namespace BlazorBaseModel.Model
{
    public class PersonneDto : GenericObject
    {
        public PersonneDto() : base() { }

        public string Nom { get; set; } = "";

        public string Prenom { get; set; } = "";

        public DateTime? DateNaissance  { get; set; } = default!;

        public string? Email { get; set; } = default!;

        public string? Tel { get; set; } = default!;

        public virtual Adresse Adresse { get; set; } = default!;
    }
}
