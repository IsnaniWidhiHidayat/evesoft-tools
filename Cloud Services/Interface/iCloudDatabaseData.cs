namespace Evesoft.CloudService
{
    public interface iCloudDatabaseData
    {
        (string,object) data{get;}
        string ToJson();
    }
}