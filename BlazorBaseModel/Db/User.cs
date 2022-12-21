using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    public class User : GenericObject
    {
        public User() : base() { }

        [Required]
        [MinLength(8),MaxLength(20)]
        public string Login { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        public string Nom { get; set; } = "";

        public string Prenom { get; set; } = "";

        public int Age { get; set; } = 0;

        public long ProfilId { get; set; } = default!;

        [ForeignKey("ProfilId")]
        public virtual Profil Profil { get; set; } = default!;

        [NotMapped]
        public string MyInfoNotInDB { get; set; }
    }
}
