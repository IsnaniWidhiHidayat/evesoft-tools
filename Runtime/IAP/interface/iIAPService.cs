using System;
using System.Collections.Generic;

namespace Evesoft.IAP
{
    public interface iIAPService
    {
        event Action<IList<iProductIAP>> onIAPInitialize;
        event Action<InitializationFailureReason> onInitializeFailed;
        event Action<iProductIAP,PurchaseFailureReason> onPurchaseFailed;
        event Action<iProductIAP> onPurchasedProduct;

        void Init(IList<iProductIAP> products);
        void BuyProduct(iProductIAP product);
    }
}
