using Newtonsoft.Json;

namespace XML_JSON
{
    public class JsonConverts
    {
        public static string GetJsonString(object p_object)
        {
            return JsonConvert.SerializeObject(p_object);
        }
        internal static T Get<T>(string p_object)
        {
            return JsonConvert.DeserializeObject<T>(p_object);
        }
    }
}