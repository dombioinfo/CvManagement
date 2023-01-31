namespace BlazorBaseApi.Provider {
    public class CsvProvider {
        private string FileName { get; set; } = default!;
        public CsvProvider() { }
        public CsvProvider(string fileName) {
            FileName = fileName;
        }
        
    }
}
