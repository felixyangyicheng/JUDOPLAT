
namespace JUDOPLAT.WASM_JUDOPLAT.Contracts
{
    public interface IPostRepo:IBaseCRUD<PostDto>
    {
        Task<List<PostDto>> GetAll();
        //Task<PagedList<PostDto>> GetAllPaged(BaseItemParameters param);
    }
}
