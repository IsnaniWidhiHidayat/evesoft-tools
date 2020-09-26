#if UNITY_IAP
using System;
using Sirenix.OdinInspector;

namespace Evesoft.IAP.Unity
{
    [Serializable,HideReferenceObjectPicker]
    public class UnityProduct : iProduct
    {
        #region private
        private UnityEngine.Purchasing.Product _unityProduct;
        private iProductDefinion _definition;
        private iProductMetadata _metadata;
        #endregion

        #region iProduct
        [ShowInInspector] public bool availableToPurchase => !_unityProduct.IsNull()? _unityProduct.availableToPurchase : false;
        [ShowInInspector] public bool hasReceipt => !_unityProduct.IsNull()? _unityProduct.hasReceipt : false;
        [ShowInInspector] public string receipt => !_unityProduct.IsNull()? _unityProduct.receipt : null;
        [ShowInInspector] public string transactionID => !_unityProduct.IsNull()? _unityProduct.transactionID : null;
        [ShowInInspector] public iProductDefinion definition => !_unityProduct.IsNull()? _definition : null;
        [ShowInInspector] public iProductMetadata metadata => !_unityProduct.IsNull()? _metadata : null;
        #endregion
        
        #region Constructor
        public UnityProduct(UnityEngine.Purchasing.Product unityProduct)
        {
            _unityProduct = unityProduct;
            
            if(!_unityProduct.IsNull() && !_unityProduct.definition.IsNull())
                _definition = new UnityProductDefinion(_unityProduct.definition);
            
            if(!_unityProduct.IsNull() && !_unityProduct.metadata.IsNull()) 
                _metadata = new UnityProductMetadata(_unityProduct.metadata);
        }
        #endregion
    }
}
#endif