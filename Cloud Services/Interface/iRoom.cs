using System.Collections.Generic;
using System;
using UnityEngine;

namespace RollingGlory.FaceApp
{
    public interface iRoom
    {
        event Action<iRoom> onUpdated;
        
        string id{get;}
        string name{get;}
        string message{get;}
        iRoomCreator creator{get;}
        iRoomTemplate template{get;}
        IList<iRoomPhoto> photos{get;}
    }
}