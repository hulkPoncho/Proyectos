using System;

namespace AppSFTP
{
    internal class LogError
    {
        internal static void BitacoraLog(string mensaje, bool EsError=false, Exception ex=null)
        {
            if (EsError)
            {
                System.Console.WriteLine("Error: " + mensaje + ", Detalle: "+ex);
            }
            else
            {
                System.Console.WriteLine(mensaje);
            }
        }
    }
}
