using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorBaseModel.Model
{
    public class CvDto : GenericObject
    {
        public CvDto() : base() { }

        public string FileName { get; set; } = "";
        public string RelativePath { get; set; } = "";
        public int FileSize { get; set; } = 0;
        public byte[] BlobFile { get; set; } = default!;
        public long PersonneId { get; set; } = default!;
        public virtual PersonneDto? PersonneDto { get; set; } = default!;
    }
}
