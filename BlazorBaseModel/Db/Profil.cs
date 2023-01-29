using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("Profil")]
    public class Profil : GenericObject
    {
        public Profil() : base() { }

        [Column("Libelle")]
        public string Libelle { get; set; } = "";

        // public virtual ICollection<User> Users { get; set; }
    }
}
