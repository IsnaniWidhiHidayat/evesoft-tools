#if ODIN_INSPECTOR 
#if UNITY_IAP
namespace Evesoft.IAP.Unity
{
    internal class UnityPayoutDefinition : IPayoutDefinition
    {
        #region private
        private UnityEngine.Purchasing.PayoutDefinition _definition;
        #endregion

        #region iPayoutDefinition
        public PayoutType type => !_definition.IsNull() ? (PayoutType)(int) _definition.type : default(PayoutType);
        public string typeString => !_definition.IsNull() ? _definition.typeString : null;
        public string subtype => !_definition.IsNull() ? _definition.subtype : null;
        public double quantity => !_definition.IsNull() ? _definition.quantity : 0;
        public string data => !_definition.IsNull() ? _definition.data : null;
        #endregion

        #region Constructor
        public UnityPayoutDefinition(UnityEngine.Purchasing.PayoutDefinition definition)
        {
            _definition = definition;
        }
        #endregion
    }
}
#endif
#endif