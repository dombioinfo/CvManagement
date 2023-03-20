using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("ListeType")]
    public class ListeType : GenericObject
    {
        [Column("Code", TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Code { get; set; } = "";

        [Column("DefaultLibelle", TypeName = "VARCHAR")]
        [StringLength(300)]
        public string DefaultLibelle { get; set; } = "";

        public virtual ICollection<ListeItem> ListeItems { get; set; } = default!;
    }
}