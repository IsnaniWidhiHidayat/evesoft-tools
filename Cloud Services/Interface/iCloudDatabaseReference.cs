using System;

namespace RollingGlory.FaceApp
{
    public interface iCloudDatabaseReference
    {
        event Action<(string,object)> onDataAdded;
        event Action<(string,object)> onDataChange;
        event Action<(string,object)> onDataRemoved;
    }
}