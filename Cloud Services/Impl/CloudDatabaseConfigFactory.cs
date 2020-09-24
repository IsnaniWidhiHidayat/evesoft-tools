using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudDatabaseConfigFactory
    {
        public static IDictionary<string,object> CreateFirebaseDatabaseOptions(string url = null)
        {
            var options = new Dictionary<string,object>()
            {
                {nameof(url),url}
            };
            return options;
        }      
    }
}