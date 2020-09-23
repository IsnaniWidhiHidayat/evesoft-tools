using System.Collections.Generic;

namespace Evesoft.IAP
{
    public interface iProductDefinion
    {
        string id { get; }
        string storeSpecificId { get; }
        bool enabled { get; }
        ProductType type { get; }
        IEnumerable<iPayoutDefinition> payouts { get; }
        iPayoutDefinition payout { get; }
    }
}