using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


// https://mundrisoft.com/tech-bytes/c-display-name-from-property-using-reflection/
namespace ConsultaBD.Transporte
{
    public static class DisplayNameHelper
    {
        public static string GetDisplayName<T>(T clase, string llave)
        {
            var typedValue = "";
            try
            {
                var property = (from a in clase.GetType().GetProperties()
                                where
                          GetPropertyDisplayName(a) == llave
                                select a).FirstOrDefault();
                typedValue = (from a1 in property.CustomAttributes select a1.NamedArguments[0].TypedValue.ToString()).FirstOrDefault(); ;
                typedValue = typedValue.Replace(@"""", "");
            }
            catch (Exception) { }
            return typedValue;
        }

        public static string GetPropertyDisplayName(PropertyInfo pi)
        {
            var dp = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
            return dp != null ? dp.DisplayName : pi.Name;
        }
    }
}