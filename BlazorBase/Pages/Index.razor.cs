using BlazorBase.Service;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private ConfigurationService ConfigurationService { get; set; } = default!;
        private FileDto fileDto { get; set; } = default!;

        public async Task OnFileUpload(FileUploadEventArgs e)
        {
            try
            {
                fileDto = new FileDto();
                fileDto.FileName = e.File.Name;
                fileDto.FileSize = (int)e.File.Size;
                using (MemoryStream result = new MemoryStream())
                {
                    await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "tmp"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = new FileStream(Path.Combine(path, fileDto.FileName), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        await fileStream.WriteAsync(result.ToArray(), 0, fileDto.FileSize);
                    }
                    fileDto.FileBlob = result.ToArray();
                }
                await ConfigurationService.ImportFullDataAsync(fileDto);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                this.StateHasChanged();
            }
        }
    }
}
