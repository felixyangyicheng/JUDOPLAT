
namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
    public interface ICommandRepo:IBaseCRUD<Command>
    {
        Task CallUpdate();
    }
}
