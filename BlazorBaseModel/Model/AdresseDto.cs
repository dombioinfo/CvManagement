namespace BlazorBaseModel.Model
{
    public class AdresseDto : GenericObject
    {
        public AdresseDto() : base() { }

        public string Rue { get; set; } = "";

        public string Complement { get; set; } = "";

        public string Ville { get; set; } = "";

        public long CodePostal { get; set; } = default!;

        public virtual PersonneDto PersonneDto { get; set; } = default!;
    }
}
