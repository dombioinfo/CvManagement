using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("Candidature")]
    public class Candidature : GenericObject
    {
        public Candidature() : base() { }

        [Required]
        [Column("DateCandidature")]
        public DateTime DateCandidature { get; set; } = default;

        [Column("DateEntretien")]
        public DateTime? DateEntretien { get; set; } = default!;     

        [Column("PrescripteurLibelle")]
        public string PrescripteurLibelle { get; set; } = "";

        [Column("PrescripteurNom")]
        public string PrescripteurNom { get; set; } = "";

        [Column("ModaliteOrientation")]
        public string ModaliteOrientation { get; set; } = "";

        [Column("DateEntree")]
        public DateTime? DateEntree { get; set; } = default!;

        [Column("DateSortie")]
        public DateTime? DateSortie { get; set; } = default!;

        [Column("MotifRefus")]
        public string MotifRefus { get; set; } = "";

        [Column("SourceCandidature")]
        public string SourceCandidature { get; set; } = "";

        [Column("Annotation", TypeName = "VARCHAR")]
        [StringLength(5000)]
        public string Annotation { get; set; } = "";

        [Column("PersonneId")]
        public long PersonneId { get; set; } = default!;

        [ForeignKey("PersonneId")]
        public virtual Personne? Personne { get; set; } = default!;

        [Column("MetierId")]
        public long MetierId { get; set; } = default!;

        [ForeignKey("MetierId")]
        public virtual ListeItem? Metier { get; set; } = default!;

        [Column("StatutId")]
        public long? StatutId { get; set; } = default!;

        [ForeignKey("StatutId")]
        public virtual ListeItem? Statut { get; set; } = default!;

        [Column("ResultatEntretienId")]
        public long? ResultatEntretienId { get; set; } = default!;

        [ForeignKey("ResultatEntretienId")]
        public virtual ListeItem? ResultatEntretien { get; set; } = default!;
    }
}
