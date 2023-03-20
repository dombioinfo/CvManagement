using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Db
{
    [Table("Cv")]
    public class Cv : GenericObject
    {
        public Cv() : base() { }

        [Column("FileName", TypeName = "VARCHAR")]
        [StringLength(200)]
        public string FileName { get; set; } = "";

        [Column("RelativePath", TypeName = "VARCHAR")]
        [StringLength(100)]
        public string RelativePath { get; set; } = "";

        [Column("FileSize")]
        public int FileSize { get; set; } = 0;

        [Column("BlobFile")]
        public byte[] BlobFile { get; set; } = default!;

        [Column("PersonneId")]
        public long PersonneId { get; set; } = default!;

        [ForeignKey("PersonneId")]
        public virtual Personne? Personne { get; set; } = default!;
    }
}
