#if ODIN_INSPECTOR 
using System;
using System.Collections.Generic;

namespace Evesoft.IAP
{
    public interface IIAPService
    {
        event Action<IList<IProductIAP>> onIAPInitialize;
        event Action<InitializationFailureReason> onInitializeFailed;
        event Action<IProductIAP,PurchaseFailureReason> onPurchaseFailed;
        event Action<IProductIAP> onPurchasedProduct;

        void Init(IList<IProductIAP> products);
        void BuyProduct(IProductIAP product);
    }
}

#endif