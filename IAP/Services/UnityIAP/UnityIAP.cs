using UnityEngine.Purchasing;
using System.Collections.Generic;
using System;

namespace Evesoft.IAP.Unity
{
    public class UnityIAP : iIAPService, IStoreListener,IDisposable
    {
        #region Private
        private IStoreController _storeController;
        private IExtensionProvider _storeExtensionProvider;
        private IList<iProductIAP> _products;
        #endregion

        #region Constructor
        public UnityIAP(IList<iProductIAP> products)
        {
            Init(products);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            onIAPInitialize = null;
            onInitializeFailed = null;
            onPurchaseFailed = null;
            onPurchasedProduct = null;
        }
        #endregion

        #region iIAPService
        public event Action<IList<iProductIAP>> onIAPInitialize;
        public event Action<InitializationFailureReason> onInitializeFailed;
        public event Action<iProductIAP, PurchaseFailureReason> onPurchaseFailed;
        public event Action<iProductIAP> onPurchasedProduct;
        public void Init(IList<iProductIAP> products)
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            foreach (iProductIAP item in products)
            {
                if(item.IsNull() || item.id.IsNullOrEmpty())
                    continue;
                    
                builder.AddProduct(item.id, (UnityEngine.Purchasing.ProductType)((int)item.type));
            }

            _products = products;
            UnityPurchasing.Initialize(this, builder);
        }
        public void BuyProduct(iProductIAP IAPProduct)
        {
            if (IAPProduct.IsNull() || IAPProduct.product.IsNull() || !IAPProduct.product.availableToPurchase)
                return;

            _storeController.InitiatePurchase(IAPProduct.id);
        }
        #endregion

        #region Interface IStore Listener
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController         = controller;
            _storeExtensionProvider  = extensions;

            var products = controller.products.all;

            for (int i = 0; i < products.Length; i++)
            {
                var current = products[i];
                if (current.IsNull())
                    continue;
       
                iProductIAP data = _products.Find(x => x != null && x.id == current.definition.id);
                
                if(data.IsNull())
                    continue;

                iProduct product = new UnityProduct(products[i]);
                data.Init(product);
            }

            "{0} - {1}".LogFormat(this.GetType(),nameof(onIAPInitialize));
            onIAPInitialize?.Invoke(_products);
        }
        public void OnInitializeFailed(UnityEngine.Purchasing.InitializationFailureReason error)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onInitializeFailed));
            onInitializeFailed?.Invoke((InitializationFailureReason)((int)error));
        }
        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason reason)
        {
            iProductIAP productIAP = _products.IsNullOrEmpty() ? null : _products.Find(x => x != null && x.id == product.definition.id);
            if (productIAP == null)
                return;

            "{0} - {1}".LogFormat(this.GetType(),nameof(onPurchaseFailed));
            onPurchaseFailed(productIAP,(PurchaseFailureReason)((int)reason));
        }
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            iProductIAP product = _products.Find(x => x != null && x.id == e.purchasedProduct.definition.id);

            if (product != null)
            {
                "{0} - {1}".LogFormat(this.GetType(),nameof(onPurchasedProduct));
                onPurchasedProduct(product);
            }
                
            return PurchaseProcessingResult.Complete;
        }
        #endregion     
    }
}