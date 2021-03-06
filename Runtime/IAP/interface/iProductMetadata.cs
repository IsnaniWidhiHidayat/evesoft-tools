#if ODIN_INSPECTOR 
namespace Evesoft.IAP
{
    public interface IProductMetadata
    {
        string localizedPriceString { get; }
        string localizedTitle { get; }
        string localizedDescription { get; }
        string isoCurrencyCode { get; }
        decimal localizedPrice { get; }
    }
}
#endif