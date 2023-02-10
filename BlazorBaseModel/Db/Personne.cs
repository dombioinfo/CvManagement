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
        
        [JsonIgnore]
        public virtual ICollection<Adresse>? Adresses { get; set; } = default!;
    }
}
