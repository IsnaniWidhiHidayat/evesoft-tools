#if ODIN_INSPECTOR 
#if UNITY_IAP
using System;
using Sirenix.OdinInspector;

namespace Evesoft.IAP.Unity
{
    [Serializable,HideReferenceObjectPicker]
    internal class UnityProductMetadata:iProductMetadata
    {
        #region private
        UnityEngine.Purchasing.ProductMetadata _metaData;
        #endregion

        #region iProductMetadata
        [ShowInInspector] public string localizedPriceString => !_metaData.IsNull()? _metaData.localizedPriceString : null;
        [ShowInInspector] public string localizedTitle => !_metaData.IsNull()? _metaData.localizedTitle : null;
        [ShowInInspector] public string localizedDescription => !_metaData.IsNull()? _metaData.localizedDescription : null;
        [ShowInInspector] public string isoCurrencyCode => !_metaData.IsNull()? _metaData.isoCurrencyCode : null;
        [ShowInInspector] public decimal localizedPrice => !_metaData.IsNull()?_metaData.localizedPrice : 0;
        #endregion

        #region Constructor
        public UnityProductMetadata(UnityEngine.Purchasing.ProductMetadata metaData)
        {
            _metaData = metaData;
        }
        #endregion
    }
}
#endif
#endif