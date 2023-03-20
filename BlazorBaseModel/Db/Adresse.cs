using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("Adresse")]
    public class Adresse : GenericObject
    {
        public Adresse() : base() { }

        [Required]
        [Column("Rue")]
        public string Rue { get; set; } = "";

        [Column("Complement", TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Complement { get; set; } = "";

        [Required]
        [Column("Ville", TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Ville { get; set; } = "";

        [Column("CodePostal", TypeName = "VARCHAR")]
        [StringLength(10)]
        public string CodePostal { get; set; } = default!;

        [Column("PersonneId")]
        public long PersonneId { get; set; } = default!;

        [ForeignKey("PersonneId")]
        public virtual Personne? Personne { get; set; } = default!;
    }
}
