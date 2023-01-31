using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    public class Personne : GenericObject
    {

        public Personne() : base() { }
        
        [Required]
        [Column("Nom")]
        public string Nom { get; set; } = "";

        [Required]
        [Column("Prenom")]
        public string Prenom { get; set; } = "";

        [Column("DateNaissance")]
        public DateTime? DateNaissance { get; set; } = default!;

        [Column("Email")]
        public string? Email { get; set; } = default!;

        [Column("Tel")]
        public string? Tel { get; set; } = default!;

        [Column("AdresseId")]
        public long AdresseId { get; set; } = default!;

        [ForeignKey("AdresseId")]
        public virtual Adresse Adresse { get; set; } = default!;
    }
}
