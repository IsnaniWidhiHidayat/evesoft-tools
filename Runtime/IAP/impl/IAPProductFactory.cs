#if ODIN_INSPECTOR 
namespace Evesoft.IAP
{
    public static class IAPProductFactory
    {
        #if UNITY_IAP
        public static iProductIAP CreateUnityProduct(string id,int count,ProductType type)
        {
            if(id.IsNullOrEmpty())
                return null;

            return new Unity.UnityProductIAP(id,count,type);
        }
        #endif  
    }
}
#endif