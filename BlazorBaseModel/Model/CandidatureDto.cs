namespace BlazorBaseModel.Model
{
    public class CandidatureDto : GenericObject
    {
        public CandidatureDto() : base() { }
        public DateTime DateCandidature { get; set; } = default;
        public string Annotation { get; set; } = "";
        public long PersonneId { get; set; } = default!;
        public virtual PersonneDto Personne { get; set; } = default!;
        public long MetierId { get; set; } = default!;
        public virtual ListeItemDto? Metier { get; set; } = default!;

        public string SourceCandidature { get; set; } = default!;
        public string PrescripteurLibelle { get; set; } = default!; // structure
        public string PrescripteurNom { get; set; } = default!; // personne moral
        


    }
}
