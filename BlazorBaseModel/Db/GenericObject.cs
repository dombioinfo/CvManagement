using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    public abstract class GenericObject
    {
        public GenericObject() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime DateUpdate{ get; set; }

        public DateTime DateCreation { get; set; }

        public int UpdateUserId { get; set; }

        public int CreateUserId { get; set; }
    }
}
