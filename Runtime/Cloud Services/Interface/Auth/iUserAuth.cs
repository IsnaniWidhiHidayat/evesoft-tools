#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public interface IUserAuth 
    {
        string id{get;}
        string name{get;}
        string imageUrl{get;}
        string token{get;}
        string email{get;}
        CloudAuthType authType{get;}
    }
}
#endif