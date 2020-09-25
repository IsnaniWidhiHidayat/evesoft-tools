namespace Evesoft.CloudService
{
    public static class CloudAuthOptionsFactory
    {
        public static iCloudAuthOptions CreateGoogleSignIn()
        {
            return null;
        }
        public static iCloudAuthOptions CreateFacebook(){
            return null;
        }
        public static iCloudAuthOptions CreateFirebaseWithEmailPassword(string email,string password)
        {
            return new Firebase.FirebaseCloudAuthOptions(email,password);
        }
        public static iCloudAuthOptions CreateFirebaseWithFacebook(string token)
        {
            return new Firebase.FirebaseCloudAuthOptions(Firebase.FirebaseCloudAuthType.Facebook,token);
        }
        public static iCloudAuthOptions CreateFirebaseWithGoogle(string token)
        {
            return new Firebase.FirebaseCloudAuthOptions(Firebase.FirebaseCloudAuthType.GoogleSignIn,token);
        }
        public static iCloudAuthOptions CreateFirebaseWithGooglePlayService(string serverAuthCode)
        {
            return new Firebase.FirebaseCloudAuthOptions(Firebase.FirebaseCloudAuthType.GooglePlayService,serverAuthCode);
        }
    }
}