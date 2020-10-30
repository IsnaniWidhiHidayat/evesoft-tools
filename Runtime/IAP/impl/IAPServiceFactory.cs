#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.IAP
{
    public static class IAPServiceFactory
    {
        private static IDictionary<IAPServiceProvider, IIAPService> _IAPService = new Dictionary<IAPServiceProvider, IIAPService>();

        public static IIAPService Create(IList<IProductIAP> products,IAPServiceProvider provider)
        {
            if(products.IsNullOrEmpty())
                return null;

            if(_IAPService == null)
                _IAPService = new Dictionary<IAPServiceProvider, IIAPService>();

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
        public static IIAPService Get(IAPServiceProvider type)
        {
            if(_IAPService.ContainsKey(type))
                return _IAPService[type];

            return null;
        }
        public static async Task<IIAPService> GetAsync(IAPServiceProvider type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}

#endif