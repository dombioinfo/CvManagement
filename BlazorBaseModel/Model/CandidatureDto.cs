namespace BlazorBaseModel.Model
{
    public class CandidatureDto : GenericObject
    {
        public CandidatureDto() : base() { }
        public DateTime DateCandidature { get; set; } = default;
        public string Annotation { get; set; } = "";
        public long PersonneId { get; set; } = default!;
        public virtual PersonneDto Personne { get; set; } = default!;
    }
}
