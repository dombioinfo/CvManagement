using BlazorBase.Service;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace BlazorBase.Pages
{
    public partial class CvsByPersonnePage : ComponentBase
    {
        [Inject]
        private PersonneService PersonneService { get; set; } = default!;
        [Inject]
        private CvService CvService { get; set; } = default!;
        [Inject]
        public IModalService ModalService { get; set; } = default!;
        [Inject]
        public IMessageService MessageService { get; set; } = default!;
        [Parameter]
        public long PersonneId { get; set; }
        private PersonneDto Personne { get; set; } = default!;

        private IEnumerable<CvDto> Items { get; set; } = default!;
        private CvDto? SelectedItem;
        private int TotalItems { get; set; } = 0;
        private CvDto CvTmpToUploadFile { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Personne = await PersonneService.GetPersonneAsync(PersonneId);
            Items = await CvService.GetCvsByPersonneAsync(PersonneId);
            await base.OnInitializedAsync();
        }

        private async Task OnReadData(DataGridReadDataEventArgs<CvDto> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                List<CvDto> response = default!;

                // this can be call to anything, in this case we're calling a fictional api
                //var response = await Http.GetJsonAsync<Employee[]>( $"some-api/employees?page={e.Page}&pageSize={e.PageSize}" );
                if (e.ReadDataMode is DataGridReadDataMode.Virtualize)
                    response = (await CvService.GetCvsAsync()).Skip(e.VirtualizeOffset).Take(e.VirtualizeCount).ToList();
                else if (e.ReadDataMode is DataGridReadDataMode.Paging)
                    response = (await CvService.GetCvsAsync()).Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
                else
                    throw new Exception("Unhandled ReadDataMode");

                if (!e.CancellationToken.IsCancellationRequested)
                {
                    TotalItems = (await CvService.GetCvsAsync()).Count();
                    Items = new List<CvDto>(response); // an actual data for the current page
                }
            }
        }

        private void OnNewItemDefaultSetter(CvDto cvDto)
        {
            cvDto.DateCreation = DateTime.UtcNow;
            cvDto.PersonneId = PersonneId;
            StateHasChanged();
        }

        private async Task<long> OnRowInserted(SavedRowItem<CvDto, Dictionary<string, object>> e)
        {
            e.Item.BlobFile = CvTmpToUploadFile.BlobFile;
            e.Item.FileName = CvTmpToUploadFile.FileName;
            e.Item.FileSize = CvTmpToUploadFile.FileSize;
            e.Item.RelativePath = CvTmpToUploadFile.RelativePath;
            long id = await CvService.CreateCvAsync(e.Item);
            CvTmpToUploadFile = new CvDto(); // on reinit
            
            await InvokeAsync(() => StateHasChanged());
            return id;
        }

        public async Task OnRowRemoved(CvDto cvDto)
        {
            await CvService.DeleteCvAsync(cvDto.Id);
        }

        public async Task OnRowRemoving(CancellableRowChange<CvDto> e)
        {
            e.Cancel = await ShowDeleteConfirmMessage();
        }

        public async Task<bool> ShowDeleteConfirmMessage()
        {
            var confirmed = await MessageService.Confirm("Etes-vous de vouloir supprimer ?", "Confirmation");
            if (confirmed)
            {
                return false; // pas d'annulation
            }
            return true; // annulation
        }

        public async Task OnFileUpload(FileUploadEventArgs e)
        {
            CvTmpToUploadFile = new CvDto()
            {
                DateCreation = DateTime.UtcNow,
                RelativePath = "data"
            };
            try
            {
                using (MemoryStream result = new MemoryStream())
                {
                    await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);
                    CvTmpToUploadFile.FileName = e.File.Name;
                    CvTmpToUploadFile.FileSize = (int)e.File.Size;
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, CvTmpToUploadFile.RelativePath));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = new FileStream(Path.Combine(path, CvTmpToUploadFile.FileName), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        await fileStream.WriteAsync(result.ToArray(), 0, CvTmpToUploadFile.FileSize);
                    }
                    CvTmpToUploadFile.BlobFile = result.ToArray();
                    CvTmpToUploadFile.PersonneId = PersonneId;
                }
                await CvService.CreateCvAsync(CvTmpToUploadFile);
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
