using System;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudRoom
    {
        event Action<iCloudRoom,iRoom> onRoomCreated;
        event Action<iCloudRoom,string> onRoomCreatedFailed;
        event Action<iCloudRoom,iRoom> onJoinedRoom;
        event Action<iCloudRoom,string> onJoinedRoomFailed;
        event Action<iCloudRoom> onLeftRoom;

        iRoom currentRoom{get;}
        bool inited{get;}
        
        void CreateRoom(Dictionary<string,object> options);
        void JoinRoom(string id);
        void LeftRoom();
        void LeftRemoveRoom();
    }
}