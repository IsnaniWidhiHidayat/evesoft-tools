#if ODIN_INSPECTOR 
namespace Evesoft.IAP
{
    public interface iProductIAP
    {
        string id{get;}
        int count{get;}
        ProductType type{get;}
    }
}

#endif