namespace WASM_JUDOPLAT.Components.Libraries
{
    public partial class PdfViewer
    {
        #region properties
        /// <summary>
        /// dependency injection service pdf
        /// </summary>
        [Inject] IPdfRepo _pdf { get; set; }


        [Inject] NavigationManager nav { get; set; }


        [Parameter] public EventCallback<bool> UploadCompletedChanged { get; set; }
        /// <summary>
        /// text in search bar
        /// </summary>
        private string searchStringInit = "";
        /// <summary>
        /// page loading status
        /// </summary>
        private bool loading { get; set; } = false;
        public string SearchWord { get; set; }
        public int FileCount { get; set; } = 0;
        public long elapsedMs { get; set; } = 0;
        /// <summary>
        /// selected item in MudBlazor table
        /// </summary>
        private PdfModel selectedItem = null;
        /// <summary>
        /// PdfModel to be edited
        /// </summary>
        private PdfModel elementBeforeEdit;
        /// <summary>
        /// Edit trigger
        /// </summary>
        private TableEditTrigger editTrigger = TableEditTrigger.RowClick;

        /// <summary>
        /// items in MudBlazor table
        /// </summary>
        protected List<PdfModelSimple> PdfModels { get; set; }=new List<PdfModelSimple>();

        #endregion
        #region methods

        private bool FilterFunc1(PdfModel element) => FilterFunc(element, searchStringInit);

        private bool FilterFunc(PdfModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (element.FileName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        protected override async Task OnParametersSetAsync()
        {
            loading = true;
             InvokeAsync(StateHasChanged);
            PdfModels = new List<PdfModelSimple>();
            var tasks = new List<Task>();
            var b = Task.Run(async () =>
            {
                await foreach (var pdf in _pdf.GetSimpleDataAsync())
                {
                    PdfModels.Add(pdf);
                    this.StateHasChanged();

                }
            });
            Task.WhenAll(tasks);
            loading = false;
             InvokeAsync(StateHasChanged);


            base.OnParametersSetAsync();
        }




        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                FileName = ((PdfModelSimple)element).FileName,
                Category = ((PdfModelSimple)element).Category
            };
            //AddEditionEvent($"RowEditPreview event: made a backup of Element {((PdfUploadModel)element).FileName}");
        }



        private void ResetItemToOriginalValues(object element)
        {
            ((PdfModelSimple)element).FileName = elementBeforeEdit.FileName;
            ((PdfModelSimple)element).Category = elementBeforeEdit.Category;
            //((PdfUploadModel)element).Position = elementBeforeEdit.Position;
            // AddEditionEvent($"RowEditCancel event: Editing of Element {((PdfUploadModel)element).FileName} canceled");
        }

        private async Task Download(string fileName)
        {
            nav.ToAbsoluteUri($"{Endpoints.Pdf}{fileName}");
        }
        #endregion

    }
}
