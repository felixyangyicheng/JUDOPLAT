

namespace WASM_JUDOPLAT.Contracts
{
	public interface INewsRepo : IBaseCRUD<NewsDto>
    {
        Task CallUpdate();
    }
}

