

namespace WASM_JUDOPLAT.Contracts
{
    public interface IPdfRepo//:IMongoReposBase<PdfModel>
    {
         Task<PdfModel?> GetByNameAsync(string name);
         Task RemoveAsync(string name);
         Task<bool> UpdateAsync(string id, PdfModel pdfModel);
         Task<bool> CreateAsync( PdfModel pdfModel);
         Task<IList<PdfModel>> GetAsync( );

         IAsyncEnumerable<PdfModel> GetDataAsync();
         IAsyncEnumerable<PdfModelSimple> GetSimpleDataAsync();

        //public Task<PdfModel?> GetByProjectNameAndFileNameAsync(string projectname, string filename);
        //public Task<IList<PdfModel?>> GetByProjectNameAndCategoryAsync(string projectname, string category);
        //public Task<IList<PdfModel?>> GetByProjectNameAsync(string projectname);
        //public Task<IList<PdfModel?>> SearchInProjectNameByKeywordAsync(string projectname, string keyword);


    }
}
