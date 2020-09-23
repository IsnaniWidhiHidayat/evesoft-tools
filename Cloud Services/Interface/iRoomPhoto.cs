using UnityEngine;
using System;

namespace RollingGlory.FaceApp
{
    public interface iRoomPhoto
    {
        event Action<iRoomPhoto> onUpdated;
        event Action<iRoomPhoto,float> onPhotoDonwloading;
        event Action<iRoomPhoto> onPhotoDownloaded;
        event Action<iRoomPhoto,string> onPhotoDownloadFailed;
        event Action<iRoomPhoto,float> onUploading;
        event Action<iRoomPhoto> onUploadComplete;
        event Action<iRoomPhoto,string> onUploadFailed;
        event Action<iRoomPhoto> onPhotoChange;

        bool isDownloading{get;}
        bool isUploading{get;}

        string label {get;}
        string url {get;}
        Vector2 position{get;}
        Vector2 scale{get;}
        float angle{get;}

        Texture2D photo{get;}

        void SetLabel(string newLabel);
        void SetPosition(Vector2 position);
        void SetScale(Vector2 scale);
        void SetRotation(float angle);
        void SetPhoto(Texture2D texture);
    }
}