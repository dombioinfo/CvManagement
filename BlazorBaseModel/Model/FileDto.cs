namespace BlazorBaseModel.Model {
    public class FileDto : GenericObject {
        public FileDto() : base()
        {
            RelativePath = Constantes.FOLDER_DATA;
            FileBlob = new byte[0];
        }

        public string FileName { get; set; } = "";
        public string RelativePath { get; set; } = default!;
        public int FileSize { get; set; } = 0;
        public byte[] FileBlob { get; set; } = default!;
        public string FileType { get; set; } = "";
    } 
}