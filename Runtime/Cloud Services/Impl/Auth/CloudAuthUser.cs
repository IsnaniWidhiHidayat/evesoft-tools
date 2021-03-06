#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Evesoft.CloudService
{
    [Serializable,HideMonoScript,HideReferenceObjectPicker]
    internal class CloudAuthUser : IUserAuth ,IDisposable
    {
        #region private
        [SerializeField]
        private string _id;

        [SerializeField]
        private string _nickName;

        [SerializeField]
        private string _token;

        [SerializeField]
        private string _imageUrl;

        [SerializeField]
        private string _email;

        [SerializeField]
        private CloudAuthType _loginType;
        #endregion

        #region iUser
        public string id {
            get => _id;
            set => _id = value;
        }
        public string name {
            get => _nickName;
            set => _nickName = value;
        }
        public string token {
            get => _token;
            set => _token = value;
        }
        public CloudAuthType authType{
            get => _loginType;
            set => _loginType = value;
        }
        public string imageUrl {
            get => _imageUrl;
            set => _imageUrl = value;
        }
        public string  email
        {
            get => _email;
            set => _email = value;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _id         = null;
            _nickName   = null;
            _token      = null;
            _imageUrl   = null;
            _loginType  = default(CloudAuthType);
        }
        #endregion
    }
}
#endif