using System.Collections.Generic;

namespace RollingGlory.FaceApp
{
    public static class LoginOptionsFactory
    {
        public static IDictionary<string,object> CreateLoginGoogleOptions()
        {
            return null;
        }
        public static IDictionary<string,object> CreateLoginFacebookOptions()
        {
            return null;
        }
        public static IDictionary<string,object> CreateLoginFirebaseGoogleOptions(string token)
        {
            var authtype = AuthType.Google;
            var dic      = new Dictionary<string,object>()
            {
                {nameof(authtype),authtype},
                {nameof(token),token},
            };

            return dic;
        }
        public static IDictionary<string,object> CreateLoginFirebaseFacebookOptions(string token)
        {
            var authtype = AuthType.Facebook;
            var dic      = new Dictionary<string,object>()
            {
                {nameof(authtype),authtype},
                {nameof(token),token},
            };

            return dic;
        }
    }
}