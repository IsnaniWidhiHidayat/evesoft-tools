namespace RollingGlory.FaceApp
{
    public interface iUser 
    {
        string id{get;}
        string name{get;}
        string imageUrl{get;}
        string token{get;}
        string email{get;}
        AuthType authType{get;}
    }
}