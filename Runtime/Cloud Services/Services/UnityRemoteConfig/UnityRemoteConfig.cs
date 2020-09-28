#if ODIN_INSPECTOR 
#if UNITY_REMOTE_CONFIG
using System;
using System.Threading.Tasks;
using URC = Unity.RemoteConfig.ConfigManager;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.UnityRemoteConfig
{
    [HideReferenceObjectPicker]
    internal class UnityRemoteConfig<T1,T2> : iCloudRemoteConfig,IDisposable where T1:struct where T2:struct
    {        
        #region private
        private bool _fetching;
        private T1 _userAttribute;
        private T2 _appAttribute;
        #endregion

        [ShowInInspector,DisplayAsString]
        private string[] _keys => URC.appConfig.GetKeys();

        #region IDisposable
        public void Dispose()
        {
            URC.FetchCompleted -= OnFetchComplete;
        }
        #endregion

        #region iCloudRemoteConfig
        private bool _fetched = false;
        public bool isfetched => _fetched;
        public bool isHaveConfigs => !_keys.IsNullOrEmpty();
        public T GetConfig<T>(string key)
        {
            var result = default(T);

            //interger
            if(typeof(T) == typeof(int))
                URC.appConfig.GetInt(key).To<T>(out result);
            
            //Float
            if(typeof(T) == typeof(float))
                URC.appConfig.GetFloat(key).To<T>(out result);

            //Bool
            if(typeof(T) == typeof(bool))
                URC.appConfig.GetBool(key).To<T>(out result);
           
            //Long
            if(typeof(T) == typeof(long))
                URC.appConfig.GetLong(key).To<T>(out result);

            //Long
            if(typeof(T) == typeof(string))
                URC.appConfig.GetString(key).To<T>(out result);
                
            return result;
        }
        public async Task Fetch()
        {
            if(_fetching)
                return;

            URC.FetchConfigs(_userAttribute,_appAttribute);
            _fetching = true;
            await new WaitUntil(()=> !_fetching);
        }
        #endregion

        #region callback
        private void OnFetchComplete(Unity.RemoteConfig.ConfigResponse respone)
        {
            if(!_fetching)
                return;

            switch(respone.status){
                case Unity.RemoteConfig.ConfigRequestStatus.Success:
                {
                    _fetched = true;
                    break;
                }

                default:
                {
                    _fetched = false;
                    break;
                }
            }

            _fetching = false;
        }
        #endregion

        #region constructor
        public UnityRemoteConfig()
        {
            URC.FetchCompleted += OnFetchComplete;
        }
        public UnityRemoteConfig(T1 userAttribute,T2 appAtribute):this()
        {   
            this._userAttribute = userAttribute;
            this._appAttribute = appAtribute;
        }
        public UnityRemoteConfig(T1 userAttribute,T2 appAtribute,string userCustomID):this(userAttribute,appAtribute)
        {
            if(!userCustomID.IsNullOrEmpty())
                URC.SetCustomUserID(userCustomID);
        }
        #endregion
    }
}
#endif
#endif