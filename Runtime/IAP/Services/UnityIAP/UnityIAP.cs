#if ODIN_INSPECTOR 
#if UNITY_IAP
using UnityEngine.Purchasing;
using System.Collections.Generic;
using System;

namespace Evesoft.IAP.Unity
{
    internal class UnityIAP : IIAPService, IStoreListener,IDisposable
    {
        #region Private
        private IStoreController _storeController;
        private IExtensionProvider _storeExtensionProvider;
        private IList<IProductIAP> _products;
        #endregion

        #region Constructor
        public UnityIAP(IList<IProductIAP> products)
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
        public event Action<IList<IProductIAP>> onIAPInitialize;
        public event Action<InitializationFailureReason> onInitializeFailed;
        public event Action<IProductIAP, PurchaseFailureReason> onPurchaseFailed;
        public event Action<IProductIAP> onPurchasedProduct;
        public void Init(IList<IProductIAP> products)
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            foreach (IProductIAP item in products)
            {
                if(item.IsNull() || item.id.IsNullOrEmpty())
                    continue;
                    
                builder.AddProduct(item.id, (UnityEngine.Purchasing.ProductType)((int)item.type));
            }

            _products = products;
            UnityPurchasing.Initialize(this, builder);
        }
        public void BuyProduct(IProductIAP product)
        {
            var IAPProduct = product as UnityProductIAP;
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
       
                var data = _products.Find(x => x != null && x.id == current.definition.id);
                if(data.IsNull())
                    continue;

                var product = data as UnityProductIAP;
                if(product.IsNull())
                    continue;
                
                product.SetProduct(new UnityProduct(products[i]));
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
            IProductIAP productIAP = _products.IsNullOrEmpty() ? null : _products.Find(x => x != null && x.id == product.definition.id);
            if (productIAP == null)
                return;

            "{0} - {1}".LogFormat(this.GetType(),nameof(onPurchaseFailed));
            onPurchaseFailed(productIAP,(PurchaseFailureReason)((int)reason));
        }
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            IProductIAP product = _products.Find(x => x != null && x.id == e.purchasedProduct.definition.id);

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
#endif
#endif