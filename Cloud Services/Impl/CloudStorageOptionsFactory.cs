using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudStorageOptionsFactory
    {
        public static IDictionary<string,object> CreateFirebaseStorageOptions(string url = null)
        {
            var options = new Dictionary<string,object>()
            {
                {nameof(url),url}
            };
            return options;
        }      
    }
}