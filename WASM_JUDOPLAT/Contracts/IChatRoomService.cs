
namespace WASM_JUDOPLAT.Contracts
{
	public interface IChatRoomService
	{
        Task<Guid> CreateRoom(string connectionId);

        Task<Guid> GetRoomForConnectionId(string connectionId);
    }
}

