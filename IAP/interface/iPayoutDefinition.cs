namespace Evesoft.IAP
{
    public interface iPayoutDefinition
    {
        PayoutType type { get; }
        string typeString { get; }
        string subtype { get; }
        double quantity { get; }
        string data { get; }
    }
}