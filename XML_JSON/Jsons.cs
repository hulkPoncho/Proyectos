//Microsoft.AspNet.WebHelpers
//Se incluye en el marco MVC como una descarga adicional al marco .NET 

using System.Web.Helpers;

namespace XML_JSON
{
    public class Jsons
    {
        public static string GetJsonString(object p_object)
        {
            return Json.Encode(p_object);
        }
        internal static T Get<T>(string p_object)
        {
            return Json.Decode<T>(p_object);
        }
    }
}