using System.Web.Script.Serialization;

namespace XML_JSON
{
    public class JavaScriptSerializers
    {
        public static string GetJsonString(object p_object)
        {
            JavaScriptSerializer oJS = new JavaScriptSerializer();
            return oJS.Serialize(p_object);
        }
        internal static T Get<T>(string p_object)
        {
            JavaScriptSerializer oJS = new JavaScriptSerializer();
            return oJS.Deserialize<T>(p_object);
        }
    }
}