namespace BlazorBaseModel.Model
{
    public abstract class GenericObject
    {
        public long Id { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreation { get; set; }
        public int UpdateUserId { get; set; }
        public int CreateUserId { get; set; }
        public bool Actif { get; set; }
    }
}
