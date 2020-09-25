namespace Evesoft.CloudService
{
    public interface iUserAuth 
    {
        string id{get;}
        string name{get;}
        string imageUrl{get;}
        string token{get;}
        string email{get;}
        CloudAuthType authType{get;}
    }
}