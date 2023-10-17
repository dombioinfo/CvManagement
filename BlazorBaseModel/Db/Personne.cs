using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlazorBaseModel.Db
{
    public class Personne : GenericObject
    {
        public Personne() : base()
        {
            this.Adresses = new HashSet<Adresse>();
        }

        [Required]
        [Column("Nom", TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Nom { get; set; } = "";

        [Required]
        [Column("Prenom", TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Prenom { get; set; } = "";

        [Column("DateNaissance")]
        public DateTime? DateNaissance { get; set; } = default!;

        [Column("Email", TypeName = "VARCHAR")]
        [StringLength(200)]
        public string? Email { get; set; } = default!;

        [Column("Tel", TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Tel { get; set; } = default!;
        
        [JsonIgnore]
        public virtual ICollection<Adresse>? Adresses { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<Candidature>? Candidatures { get; set; } = default!;

        [JsonIgnore]
        public virtual ICollection<Cv>? Cvs { get; set; } = default!; 
    }
}
