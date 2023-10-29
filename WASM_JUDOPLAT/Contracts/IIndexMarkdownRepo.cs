
namespace WASM_JUDOPLAT.Contracts
{
	public interface IIndexMarkdownRepo : IBaseCRUD<IndexMarkdownDto>
    {
        Task CallUpdate();
    }
}

