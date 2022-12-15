using BlazorBaseModel.Db;

namespace BlazorBaseModel.Model
{
    public class User : GenericObject
    {
        public User() : base() { }

        public string Login { get; set; } = "";

        public string Nom { get; set; } = "";

        public string Prenom { get; set; } = "";

        public int Age { get; set; } = 0;

        public virtual Profil Profil { get; set; } = default!;
    }
}
