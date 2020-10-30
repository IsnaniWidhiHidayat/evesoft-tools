#if ODIN_INSPECTOR 
using System;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface ICloudRoom
    {
        event Action<ICloudRoom,IRoom> onRoomCreated;
        event Action<ICloudRoom,string> onRoomCreatedFailed;
        event Action<ICloudRoom,IRoom> onJoinedRoom;
        event Action<ICloudRoom,string> onJoinedRoomFailed;
        event Action<ICloudRoom> onLeftRoom;

        IRoom currentRoom{get;}
        bool inited{get;}
        
        void CreateRoom(Dictionary<string,object> options);
        void JoinRoom(string id);
        void LeftRoom();
        void LeftRemoveRoom();
    }
}
#endif