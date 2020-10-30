#if ODIN_INSPECTOR 
namespace Evesoft.IAP
{
    public interface IPayoutDefinition
    {
        PayoutType type { get; }
        string typeString { get; }
        string subtype { get; }
        double quantity { get; }
        string data { get; }
    }
}
#endif