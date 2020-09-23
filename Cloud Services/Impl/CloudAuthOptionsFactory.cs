using System.Collections.Generic;

namespace RollingGlory.FaceApp
{
    public static class CloudAuthOptionsFactory
    {
        public static IDictionary<string,object> CreateGoogleAuthOptions(string webclientid)
        {
            var options = new Dictionary<string,object>()
            {
                {nameof(webclientid),webclientid}
            };
            return options;
        }
        public static IDictionary<string,object> CreateFacebookAuthOptions()
        {
            return null;
        }
        public static IDictionary<string,object> CreateFirebaseAuthOptions()
        {
            return null;
        }
    }
}