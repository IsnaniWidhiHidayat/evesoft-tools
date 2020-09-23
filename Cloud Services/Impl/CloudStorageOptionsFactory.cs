using System.Collections.Generic;

namespace RollingGlory.FaceApp
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