
public class NewtonsoftJsonPacker 
{
    public static string Pack<T>(T obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }

    public static T Unpack<T>(string json)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
}
