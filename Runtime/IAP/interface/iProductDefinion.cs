#if ODIN_INSPECTOR 
using System.Collections.Generic;

namespace Evesoft.IAP
{
    public interface IProductDefinion
    {
        string id { get; }
        string storeSpecificId { get; }
        bool enabled { get; }
        ProductType type { get; }
        IEnumerable<IPayoutDefinition> payouts { get; }
        IPayoutDefinition payout { get; }
    }
}
#endif