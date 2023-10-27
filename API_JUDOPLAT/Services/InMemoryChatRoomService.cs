using System;

namespace JUDOPLAT.API_JUDOPLAT.Services
{
	public class InMemoryChatRoomService: IChatRoomService
    {
        private readonly Dictionary<Guid, ChatRoom> _roomInfo
    = new Dictionary<Guid, ChatRoom>();

        public Task<Guid> CreateRoom(string connectionId)
        {
            var id = Guid.NewGuid();
            _roomInfo[id] = new ChatRoom
            {
                OwnerConnectionId = connectionId
            };

            return Task.FromResult(id);
        }

        public Task<Guid> GetRoomForConnectionId(string connectionId)
        {
            var foundRoom = _roomInfo.FirstOrDefault(
                x => x.Value.OwnerConnectionId == connectionId);

            if (foundRoom.Key == Guid.Empty)
                throw new ArgumentException("Invalid connection ID");

            return Task.FromResult(foundRoom.Key);
        }
    }
}

