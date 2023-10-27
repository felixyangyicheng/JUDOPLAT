
namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
    public interface IPostRepo:IBaseCRUD<Post>
    {
        Task<List<Post>> GetAll();
       // Task<PagedList<PostDto>> GetAllPaged(BaseItemParameters param);
    }
}
