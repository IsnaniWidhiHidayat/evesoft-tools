#if ODIN_INSPECTOR 
namespace Evesoft.IAP
{
    public interface IProduct
    {
        bool availableToPurchase{get;}
        bool hasReceipt{get;}
        string receipt{get;}
        string transactionID{get;}
        IProductDefinion definition {get;}
        IProductMetadata metadata{get;}
    }
}
#endif