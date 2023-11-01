

namespace WASM_JUDOPLAT.Contracts
{
    public interface IPdfRepo//:IMongoReposBase<PdfModel>
    {
        public Task<PdfModel?> GetByNameAsync(string name);
        public Task RemoveAsync(string name);
        public Task<bool> UpdateAsync(string id, PdfModel pdfModel);
        public Task<bool> CreateAsync( PdfModel pdfModel);
        public Task<IList<PdfModel>> GetAsync( );
        //public Task<PdfModel?> GetByProjectNameAndFileNameAsync(string projectname, string filename);
        //public Task<IList<PdfModel?>> GetByProjectNameAndCategoryAsync(string projectname, string category);
        //public Task<IList<PdfModel?>> GetByProjectNameAsync(string projectname);
        //public Task<IList<PdfModel?>> SearchInProjectNameByKeywordAsync(string projectname, string keyword);


    }
}
