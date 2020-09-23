namespace EveSoft.IAP
{
    public interface iProductMetadata
    {
        string localizedPriceString { get; }
        string localizedTitle { get; }
        string localizedDescription { get; }
        string isoCurrencyCode { get; }
        decimal localizedPrice { get; }
    }
}