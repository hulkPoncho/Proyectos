using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XML_JSON
{
    public class JObjects
    {
        public static string GetJsonString(JObject p_object)
        {
            return p_object.ToString(Formatting.None);
        }
        internal static JObject Get(string p_object)
        {
            return JObject.Parse(p_object);
        }
    }
}