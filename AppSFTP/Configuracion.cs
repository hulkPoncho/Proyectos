using System.Collections.Generic;
using System.Linq;

namespace AppSFTP
{
    public class Configuracion
    {


        private static string ObtenerValor(string Key)
        {
            var valorLlave =
                System.Configuration.ConfigurationSettings.AppSettings[Key];
            return valorLlave;
        }

        private static bool ObtenerValorBool(string Key)
        {
            var valor = ObtenerValor(Key);
            bool flag;
            bool.TryParse(valor, out flag);
            return flag;
        }

        private static int ObtenerValorInt(string Key)
        {
            int valorInt;
            var valor = ObtenerValor(Key);
            int.TryParse(valor, out valorInt);
            return valorInt;
        }

        private static int ObtenerValorPort()
        {
            int valorInt=ObtenerValorInt("Port");
            return valorInt==0?22:valorInt;
        }

        private static int ObtenerValorOperationTimeout()
        {
            int valorInt = ObtenerValorInt("OperationTimeout");
            return valorInt == 0 ? 40 : valorInt;
        }

        public static string HostName { get { return ObtenerValor("HostName"); } }
        public static string UserName { get { return ObtenerValor("UserName"); } }
        public static string Password { get { return ObtenerValor("Password"); } }
        public static string RemoteDirectory { get { return ObtenerValor("RemoteDirectory"); } }
        public static string LocalDirectory { get { return ObtenerValor("LocalDirectory"); } }
        public static bool RemoteDelete { get { return ObtenerValorBool("RemoteDelete"); } }
        public static bool LocalDelete { get { return ObtenerValorBool("LocalDelete"); } }
        public static int Port { get { return ObtenerValorPort(); } }
        public static int OperationTimeout { get { return ObtenerValorOperationTimeout(); } }
    }
}
