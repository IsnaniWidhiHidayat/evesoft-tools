#if ODIN_INSPECTOR 
#if NATIVE_SHARE
using System;
using UnityEngine;

namespace Evesoft.Share.NativeShare
{
    public class NativeShare : IShare,IDisposable
    {
        #region private
        private global::NativeShare _nativeShare;
        #endregion

        #region constructor
        public NativeShare()
        {
            _nativeShare = new global::NativeShare();
        }
        #endregion
        
        public void Dispose()
        {
            _nativeShare.Dispose();
            _nativeShare = null;
        }

        #region iShare
        public IShare AddTexture(Texture2D texture)
        {
            if(!texture.IsNull())
            {
               var bytes = texture.EncodeToPNG();
               if(!bytes.IsNullOrEmpty())
               {
                    var fileName = "TempShare.png";
                    var path = Application.temporaryCachePath.CombinePath(fileName);
                    bytes.WriteToFile(path);
                    return AddFile(path);
               }
            }

            return this;
        }
        public IShare AddFile(string filePath, string mime = null)
        {
            _nativeShare.AddFile(filePath,mime);
            return this;        
        }
        public IShare SetSubject(string subject)
        {
            _nativeShare.SetSubject(subject);
            return this;
        }
        public IShare SetTarget(string androidPackageName, string androidClassName = null)
        {
            _nativeShare.AddTarget(androidClassName,androidClassName);
           return this;
        }
        public IShare SetText(string text)
        {
            _nativeShare.SetText(text);
            return this;
        }
        public IShare SetTitle(string title)
        {
            _nativeShare.SetTitle(title);
            return this;
        }
        public void Share()
        {
            _nativeShare.Share();
        }
        #endregion
    }
}

#endif
#endif