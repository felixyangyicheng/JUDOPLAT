

namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
	public interface INewsRepo : IBaseCRUD<News>
    {
        Task CallUpdate();
    }
}

