namespace BlazorBaseModel.Db
{
    public class Profil : GenericObject
    {
        public Profil() : base() { }

        public string Libelle { get; set; } = "";

        // public virtual ICollection<User> Users { get; set; }
    }
}
