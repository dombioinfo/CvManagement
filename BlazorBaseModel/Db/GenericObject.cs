using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    public abstract class GenericObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)] // pas directement compatible avec sqlite
        public DateTime DateUpdate { get; set; }

        /// <summary>
        /// Ce champ est alimenté par une valeur par défaut définit dans <object>SqliteDbContext</object>.<method>OnModelCreating</method>
        /// </summary>
        public DateTime DateCreation { get; set; }

        public int? UpdateUserId { get; set; }

        public int? CreateUserId { get; set; }

        public bool Actif { get; set; }
    }
}
