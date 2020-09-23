namespace EveSoft.IAP
{
    public interface iProductIAP
    {
        string id{get;set;}
        ProductType type{get;set;}
        int itemCount{get;set;}
        iProduct product {get;}
        void Init(iProduct product);
    }
}
