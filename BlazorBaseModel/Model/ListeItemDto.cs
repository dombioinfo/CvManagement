
namespace BlazorBaseModel.Model
{
    public class ListeItemDto : GenericObject
    {
        public ListeItemDto() : base() { }
        
        public string Code { get; set; } = "";
        public string DefaultLibelle { get; set; } = "";
        public long ListeTypeDtoId { get; set; }
    }
}