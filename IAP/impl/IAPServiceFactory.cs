using System.Collections.Generic;

namespace EveSoft.IAP
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
                case IAPServiceProvider.Unity:
                {
                    return _IAPService[provider] = new Unity.UnityIAP(products);
                }

                default:
                {
                    return null;
                }
            }
        }
    }
}
