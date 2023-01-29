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

        [Column("Complement")]
        public string Complement { get; set; } = "";

        [Required]
        [Column("Ville")]
        public string Ville { get; set; } = "";

        [Column("CodePostal")]
        public long CodePostal { get; set; } = default!;
    }
}
