#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.IAP
{
    public static class IAPServiceFactory
    {
        private static IDictionary<IAPServiceProvider, iIAPService> _IAPService = new Dictionary<IAPServiceProvider, iIAPService>();

        public static iIAPService Create(IList<iProductIAP> products,IAPServiceProvider provider)
        {
            if(products.IsNullOrEmpty())
                return null;

            if(_IAPService == null)
                _IAPService = new Dictionary<IAPServiceProvider, iIAPService>();

            if (_IAPService.ContainsKey(provider))
                return _IAPService[provider];

            switch (provider)
            {
                #if UNITY_IAP
                case IAPServiceProvider.Unity:
                {
                    return _IAPService[provider] = new Unity.UnityIAP(products);
                } 
                #endif
                
                default:
                {
                    "Service Not Available".LogError();
                    return null;
                }
            }
        }
        public static iIAPService Get(IAPServiceProvider type)
        {
            if(_IAPService.ContainsKey(type))
                return _IAPService[type];

            return null;
        }
        public static async Task<iIAPService> GetAsync(IAPServiceProvider type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}

#endif