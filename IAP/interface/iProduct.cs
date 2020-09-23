namespace EveSoft.IAP
{
    public interface iProduct
    {
        bool availableToPurchase{get;}
        bool hasReceipt{get;}
        string receipt{get;}
        string transactionID{get;}
        iProductDefinion definition {get;}
        iProductMetadata metadata{get;}
    }
}