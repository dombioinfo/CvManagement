using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("User")]
    public class User : GenericObject
    {
        public User() : base() { }

        [Required]
        [MinLength(8), MaxLength(20)]
        [Column("Login")]
        public string Login { get; set; } = "";

        [Required]
        [Column("Password")]
        public string Password { get; set; } = "";

        [Required]
        [Column("Nom")]
        public string Nom { get; set; } = "";

        [Column("Prenom")]
        public string Prenom { get; set; } = "";

        [Column("Age")]
        public int Age { get; set; } = 0;

        [Column("ProfilId")]
        public long ProfilId { get; set; } = default!;

        [ForeignKey("ProfilId")]
        public virtual Profil Profil { get; set; } = default!;

        [NotMapped]
        public string MyInfoNotInDB { get; set; } = default!;
    }
}
