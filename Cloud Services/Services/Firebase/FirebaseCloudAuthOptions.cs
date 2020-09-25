using System.Collections.Generic;

namespace Evesoft.CloudService.Firebase
{
    public class FirebaseCloudAuthOptions : iCloudAuthOptions
    {
        #region const
        public const string EMAIL = nameof(EMAIL);
        public const string PASSWORD = nameof(PASSWORD);
        public const string TOKEN = nameof(TOKEN);
        public const string LOGIN_TYPE = nameof(LOGIN_TYPE);
        #endregion

        #region private
        private IDictionary<string,object> _configs;
        #endregion

        #region iCloudAuthOptions
        public T GetOptions<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion

        #region constructor
        public FirebaseCloudAuthOptions(FirebaseCloudAuthType type)
        {
            _configs = new Dictionary<string, object>();
            _configs[LOGIN_TYPE] = type;
        }
        public FirebaseCloudAuthOptions(string email,string password):this(FirebaseCloudAuthType.EmailPassword)
        {
            _configs[EMAIL]         = email;
            _configs[PASSWORD]      = password;
        }
        public FirebaseCloudAuthOptions(FirebaseCloudAuthType type, string token):this(type)
        {
            _configs[TOKEN] = token;
        }
        #endregion
    }
}