

namespace JUDOPLAT.WASM_JUDOPLAT.Contracts
{
    public interface ICommentRepo:IBaseCRUD<CommentDto>
    {
        Task CallUpdate();
    }
}
