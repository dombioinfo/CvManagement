using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    public abstract class GenericObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public long Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("DateUpdate")]
        public DateTime DateUpdate { get; set; }

        /// <summary>
        /// Ce champ est alimenté par une valeur par défaut définit dans <object>MysqlDbContext</object>.<method>OnModelCreating</method>
        /// </summary>
        [Column("DateCreation")]
        public DateTime DateCreation { get; set; }

        [Column("UpdateUserId")]
        public int UpdateUserId { get; set; }

        [Column("CreateUserId")]
        public int CreateUserId { get; set; }
    }
}
