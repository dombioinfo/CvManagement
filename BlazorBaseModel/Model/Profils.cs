using BlazorBaseModel.Db;

namespace BlazorBaseModel.Model
{
    public class Profil : GenericObject
    {
        public Profil() : base() { }

        public string Libelle { get; set; } = "";

    }
}
