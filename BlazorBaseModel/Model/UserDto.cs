namespace BlazorBaseModel.Model
{
    public class UserDto : GenericObject
    {
        public UserDto() : base() { }

        public string Login { get; set; } = "";

        public string Nom { get; set; } = "";

        public string Prenom { get; set; } = "";

        public int Age { get; set; } = 0;

        public virtual ProfilDto Profil { get; set; } = default!;
    }
}
