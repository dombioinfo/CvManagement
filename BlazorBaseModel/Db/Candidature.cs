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

        [Column("Annotation")]
        public string Annotation { get; set; } = "";

        [Column("PersonneId")]
        public long PersonneId { get; set; } = default!;

        [ForeignKey("PersonneId")]
        public virtual Personne? Personne { get; set; } = default!;
    }
}
