#if ODIN_INSPECTOR 
using UnityEngine;

namespace Evesoft.Share
{
    public interface IShare 
    {
        IShare SetSubject(string subject);
        IShare SetText(string text);
        IShare SetTitle(string title);
        IShare SetTarget(string androidPackageName,string androidClassName = null);
        IShare AddTexture(Texture2D texture);
        IShare AddFile(string filePath,string mime = null);
        void Share();
    }
}
#endif