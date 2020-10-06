namespace Evesoft.IAP.Unity
{
    internal class UnityProductIAP : iProductIAP
    {
        #region private
        internal iProduct product{get;private set;}
        #endregion

        internal void SetProduct(iProduct product)
        {
            this.product = product;
        }


        #region iProductIAP
        public string id {get;private set;}
        public int count {get;private set;}
        public ProductType type {get;private set;}
        #endregion

        #region constructor
        public UnityProductIAP(string id,int count ,ProductType type)
        {
            this.id = id;
            this.count = count;
            this.type = type;
        }
        #endregion
    }
}