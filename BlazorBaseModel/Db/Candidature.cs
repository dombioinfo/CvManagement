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
        public long StatutId { get; set; } = default!;

        [ForeignKey("StatutId")]
        public virtual ListeItem? Statut { get; set; } = default!;
    }
}
