namespace RollingGlory.FaceApp
{
    public interface iCloudDatabaseData
    {
        (string,object) data{get;}
        string ToJson();
    }
}