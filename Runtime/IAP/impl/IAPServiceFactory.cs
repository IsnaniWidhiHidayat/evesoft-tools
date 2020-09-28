#if ODIN_INSPECTOR 
using System.Collections.Generic;

namespace Evesoft.IAP
{
    public static class IAPServiceFactory
    {
        private static Dictionary<IAPServiceProvider, iIAPService> _IAPService;

        public static iIAPService CreteIAPService(IList<iProductIAP> products,IAPServiceProvider provider)
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
    }
}

#endif