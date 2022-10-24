using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecorteApp
{
    internal class Program
    {
        private static void GeneracionPDF(string origen, string destino, string ejecutable, string origenTiempo, string finTiempo)
        {
            // https://stackoverflow.com/questions/9913032/how-can-i-extract-audio-from-video-with-ffmpeg
            // https://www.ffmpeg.org/download.html

            //ffmpeg - i "C:\Users\flord\Desktop\VIDEOS\inauguracion-y-conferencia-alumnos-destacados.mp4" - map 0 - default_mode infer_no_subs - ss 00:00:00 - to 00:41:00 - c copy "C:\Users\flord\Desktop\VIDEOS\b.mp4"
            //ffmpeg - i "video.mp4" - map 0 - default_mode infer_no_subs - ss 00:00:00 - to 00:41:00 - c copy "video.mp4"
            destino = destino.ToUpper();
            destino = destino.Replace(".MP3", "_.MP3");
            var cadena = " -i \"" + origen + "\" -vn -acodec copy -ss " + origenTiempo + " -to " + finTiempo + " \"" + destino + "\"";
            // acodec - codec de audio, vcodec - codec de video

            // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo?view=net-6.0
            var compiler = new Process
            {
                StartInfo =
                {
                    Arguments=cadena, // Obtiene o establece el conjunto de argumentos de la línea de comandos que se usará al iniciar la aplicación.
                    FileName=ejecutable, // Obtiene o establece la aplicación o el documento para iniciar.
                    RedirectStandardOutput=true, // Obtiene o establece un valor que indica si la salida textual de una aplicación se escribe en el flujo StandardOutput.
                    UseShellExecute=false, // Obtiene o establece un valor que indica si se debe usar el shell del sistema operativo para iniciar el proceso.
                    RedirectStandardError=true, // Obtiene o establece un valor que indica si la salida de error de una aplicación se escribe en la secuencia StandardError.
                    CreateNoWindow=true // Obtiene o establece un valor que indica si se debe iniciar el proceso en una ventana nueva.
                }
            };
            compiler.Start();
            compiler.WaitForExit();
            var mensajeSalida = compiler.StandardOutput;
        }

        static void Main(string[] args)
        {
            var PATH = @"C:\Prueba";
            var xou = System.IO.Path.Combine(PATH, "DLD - MI VIDA.MP3");
            Console.WriteLine(xou);
            var origenTiempo = "00:54";
            var finTiempo = "05:18";
            origenTiempo = "00:" + origenTiempo;
            finTiempo = "00:" + finTiempo;
            //ffmpeg.exe - i "G:\\MUSICA1\\autumn midnight\\DEDICATORIA ESPECIAL - Seemann _ LoFi Version.mp3" - vn - acodec copy - ss 00:00:07 - to 00:04:39 "G:\\MUSICA1\\AUTUMN MIDNIGHT\\DEDICATORIA ESPECIAL - SEEMANN _ LOFI VERSION_.MP3"
            GeneracionPDF(xou, xou, @"C:\Users\flord\Downloads\ffmpeg-n5.1-latest-win64-lgpl-shared-5.1\ffmpeg-n5.1-latest-win64-lgpl-shared-5.1\bin\ffmpeg.exe", origenTiempo, finTiempo);
        }
    }
}
