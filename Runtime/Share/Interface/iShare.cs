using UnityEngine;

namespace Evesoft.Share
{
    public interface iShare 
    {
        iShare SetSubject(string subject);
        iShare SetText(string text);
        iShare SetTitle(string title);
        iShare SetTarget(string androidPackageName,string androidClassName = null);
        iShare AddTexture(Texture2D texture);
        iShare AddFile(string filePath,string mime = null);
        void Share();
    }
}