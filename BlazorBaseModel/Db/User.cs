using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    public class User : GenericObject
    {
        public User() : base() { }

        public string Login { get; set; } = "";

        public string Password { get; set; } = "";

        public string Nom { get; set; } = "";

        public string Prenom { get; set; } = "";

        public int Age { get; set; } = 0;

        // [ForeignKey("Profil")]
        public long ProfilId { get; set; } = default!;
        public virtual Profil Profil { get; set; } = default!;
    }
}
