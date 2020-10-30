#if ODIN_INSPECTOR 
#if UNITY_IAP
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.IAP.Unity
{
    [Serializable, HideReferenceObjectPicker]
    internal class UnityProductDefinion : IProductDefinion
    {
        #region private
        private UnityEngine.Purchasing.ProductDefinition _definition;
        private IEnumerable<IPayoutDefinition> _payouts;
        private IPayoutDefinition _payout;
        #endregion

        #region iProductDefinion
        public string id => !_definition.IsNull()? _definition.id : null;
        public string storeSpecificId => !_definition.IsNull()? _definition.storeSpecificId : null;
        public bool enabled => !_definition.IsNull()? _definition.enabled : false;
        public ProductType type => !_definition.IsNull()? (ProductType)(int)_definition.type : default(ProductType);
        public IEnumerable<IPayoutDefinition> payouts => !_definition.IsNull()? _payouts : null; 
        public IPayoutDefinition payout => !_definition.IsNull()? _payout : null;
        #endregion

        #region Constructor
        public UnityProductDefinion(UnityEngine.Purchasing.ProductDefinition definition)
        {
            _definition = definition;

            if(!_definition.IsNull() && !definition.payout.IsNull())
                _payout = new UnityPayoutDefinition(_definition.payout);
    
            if(!_definition.payouts.IsNull()) 
            {
                var payouts = default(List<IPayoutDefinition>);
                
                foreach (var payout in _definition.payouts)
                {
                    if(payout.IsNull())
                        return;

                    if(payouts.IsNull())
                        payouts = new List<IPayoutDefinition>();

                    payouts.Add(new UnityPayoutDefinition(payout));
                }

                _payouts = payouts;
            }
        }
        #endregion
    }
}
#endif
#endif