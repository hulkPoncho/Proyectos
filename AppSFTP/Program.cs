using System;

namespace AppSFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConexionSSH.Contador = 0;

                var argumento = args != null && args.Length > 0 ? args[0] : "";
                argumento = "-d";
                EjecucionCase(argumento);
            }
            catch (Exception ex)
            {
                LogError.BitacoraLog("Presione enter para continuar...",true,ex);
                Console.ReadLine();
            }
        }

        private static string EjecucionOrquesta(string argumento)
        {
            string error;
            switch (argumento)
            {
                case "-d": error = ConexionSSH.DescargarArchivos(); break;
                case "-l": error = ConexionSSH.ListadoArchivos(); break;
                case "-u": error = ConexionSSH.SubirArchivos(); break;
                default: error = ""; break;
            }
            return error;
        }

        private static string EjecucionAccion(string argumento)
        {
            string accion;
            switch (argumento)
            {
                case "": accion = "DESCARGA"; break;
                case "-l": accion = "LISTADO"; break;
                case "-u": accion = "CARGA"; break;
                case "-c": accion = "CONFIGURACION"; break;
                default: accion = "DESCONOCIDA"; break;
            }
            return accion;
        }

        private static void EjecucionCase(string argumento)
        {
            var error = EjecucionOrquesta(argumento);
            ConexionSSH.Contador = 0;
            if (error != "")
            {
                LogError.BitacoraLog("Error global en la ejecución: " + error, true);
            }
            else
            {
                var accion = EjecucionAccion(argumento);
                LogError.BitacoraLog("Ejecución exitosa de la acción: [" + accion + "]", false);
                if (accion == "CARGA" || accion == "DESCARGA")
                {
                    LogError.BitacoraLog("Archivos procesados: " + ConexionSSH.Contador, false);
                }
            }
        }
    }
}
