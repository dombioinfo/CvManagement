namespace BlazorBaseModel.Model
{
    public class CandidatureDto : GenericObject
    {
        public CandidatureDto() : base() { }
        public DateTime DateCandidature { get; set; } = default;
        public DateTime? DateEntretien { get; set; } = default!;
        public string PrescripteurLibelle { get; set; } = string.Empty; // structure
        public string PrescripteurNom { get; set; } = string.Empty; // personne moral
        public string ModaliteOrientation { get; set; } = string.Empty;
        public DateTime? DateEntree { get; set; } = default!;
        public DateTime? DateSortie { get; set; } = default!;
        public string MotifRefus { get; set; } = string.Empty;
        public string SourceCandidature { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public long PersonneId { get; set; } = default!;
        public virtual PersonneDto Personne { get; set; } = default!;
        public long MetierId { get; set; } = default!;
        public virtual ListeItemDto? Metier { get; set; } = default!;
        public long? StatutId { get; set; } = default!;
        public virtual ListeItemDto? Statut { get; set; } = default!;
        public long? ResultatEntretienId { get; set; } = default!;
        public virtual ListeItemDto? ResultatEntretien { get; set; } = default!;
    }
}
