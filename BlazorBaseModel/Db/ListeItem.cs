using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("ListeItem")]
    public class ListeItem : GenericObject
    {
        [Column("Code", TypeName = "VARCHAR")]
        [StringLength(150)]
        public string Code { get; set; } = "";

        [Column("DefaultLibelle", TypeName = "VARCHAR")]
        [StringLength(300)]
        public string DefaultLibelle { get; set; } = "";

        [ForeignKey("ListeTypeId")]
        public long ListeTypeId { get; set; }

        //public virtual ListeType? ListeType { get; set; } = default!;
    }
}