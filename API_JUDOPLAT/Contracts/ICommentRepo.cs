

namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
    public interface ICommentRepo:IBaseCRUD<Comment>
    {
        Task CallUpdate();
    }
}
