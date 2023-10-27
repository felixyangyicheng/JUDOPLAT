
namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
	public interface IIndexMarkdownRepo : IBaseCRUD<IndexMarkdown>
    {
        Task CallUpdate();
    }
}

