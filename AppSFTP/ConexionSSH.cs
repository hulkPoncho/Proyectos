using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.IO;
using System.Linq;

namespace AppSFTP
{
    class ConexionSSH
    {
        public static int Contador;

        /// <summary>
        /// Enumere un directorio remoto en la consola.
        /// </summary>
        public static string ListadoArchivos()
        {
            var error = "";
            var host = Configuracion.HostName;
            var username = Configuracion.UserName;
            var password = Configuracion.Password;
            var remoteDirectory = Configuracion.RemoteDirectory;
            try
            {
                var connectionInfo = new PasswordConnectionInfo(host, Configuracion.Port, username, password);

                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.OperationTimeout = TimeSpan.FromSeconds(Configuracion.OperationTimeout);
                    sftp.KeepAliveInterval = sftp.OperationTimeout;
                    sftp.Connect();
                    ListadoArchivosRecursivo(remoteDirectory, sftp);
                    LogError.BitacoraLog("******************************************************************************");
                    LogError.BitacoraLog("Fin de listado");
                    LogError.BitacoraLog("******************************************************************************");
                    if (sftp.IsConnected)
                    {
                        sftp.Disconnect();
                    }
                }
            }
            catch (Exception e)
            {
                error = "Se ha detectado una excepción al listar el directorio: " + remoteDirectory + ", Excepción: " + e.ToString();
                LogError.BitacoraLog("******************************************************************************");
                LogError.BitacoraLog(error);
                LogError.BitacoraLog("******************************************************************************");
            }

            return error;
        }

        /// <summary>
        /// Enumere el listado de un directorio remoto de manera recursiva
        /// </summary>
        private static void ListadoArchivosRecursivo(string remoteDirectory, SftpClient sftp)
        {
            var leyenda = "Contenido de directorio: " + remoteDirectory;
            LogError.BitacoraLog("******************************************************************************");
            LogError.BitacoraLog(leyenda);
            LogError.BitacoraLog("******************************************************************************");
            
            var files = sftp.ListDirectory(remoteDirectory);
            var listado = (from a in files select a).OrderBy(s => s.FullName).ToList();
            
            foreach (var file in listado)
            {
                if (file.IsDirectory)
                {
                    if (file.Name != "." && file.Name != "..")
                    {
                        leyenda = "Directorio: " + file.Name;
                        LogError.BitacoraLog(leyenda);
                        ListadoArchivosRecursivo(file.FullName, sftp);
                    }
                }
                else
                {
                    leyenda = "Archivo: " + file.Name;
                    LogError.BitacoraLog(leyenda);
                }
            }
        }

        /// <summary>
        /// Descarga del contenido de un directorio remoto
        /// </summary>
        public static string DescargarArchivos()
        {
            var error = "";
            var host = Configuracion.HostName;
            var username = Configuracion.UserName;
            var password = Configuracion.Password;
            var remoteDirectory = Configuracion.RemoteDirectory;
            var LocalDirectory = Configuracion.LocalDirectory;

            LogError.BitacoraLog("******************************************************************************");
            LogError.BitacoraLog("Comienza descarga");
            LogError.BitacoraLog("******************************************************************************");

            try
            {
                var connectionInfo = new PasswordConnectionInfo(host, Configuracion.Port, username, password);
                using (var sftp = new SftpClient(connectionInfo))
                {
                    //sftp.ConnectionInfo.Timeout = TimeSpan.FromSeconds(1000);
                    sftp.OperationTimeout = TimeSpan.FromSeconds(Configuracion.OperationTimeout);
                    sftp.KeepAliveInterval = sftp.OperationTimeout;

                    sftp.ConnectionInfo.Timeout = TimeSpan.FromSeconds(1);
                    sftp.ConnectionInfo.RetryAttempts = 2;
                    sftp.OperationTimeout = TimeSpan.FromSeconds(1);
                    sftp.KeepAliveInterval = TimeSpan.FromSeconds(1);

                    sftp.Connect();
                    DescargaArchivosRecursivo(sftp, remoteDirectory, LocalDirectory, true);
                    LogError.BitacoraLog("******************************************************************************");
                    LogError.BitacoraLog("Fin de la descarga");
                    LogError.BitacoraLog("******************************************************************************");
                    if (sftp.IsConnected)
                    {
                        sftp.Disconnect();
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                System.Console.ReadLine();
                error = "Se ha detectado una excepción al descargar el directorio: " + remoteDirectory + ", Excepción: " + e.ToString();
                LogError.BitacoraLog("******************************************************************************");
                LogError.BitacoraLog(error);
                LogError.BitacoraLog("******************************************************************************");
            }
            return error;
        }

        /// <summary>
        /// Descarga un directorio remoto en un directorio local
        /// </summary>
        /// <param name="client"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private static void DescargaArchivosRecursivo(SftpClient client, string source, string destination, bool recursive)
        {
            // Enumere los archivos y carpetas del directorio
            var listado = client.ListDirectory(source);
            var conteoArchivos = (from a in listado where !a.IsDirectory select a).Count();

            // Iterar sobre ellos
            var increment = 0;
            foreach (SftpFile file in listado)
            {
                // Si es un archivo, descárgalo
                if (!file.IsDirectory && !file.IsSymbolicLink)
                {
                    increment++;
                    ProcesarArchivoRecursivo(increment + " de " + conteoArchivos, client, file, destination);
                }
                // Si es un enlace simbólico, ignórelo
                else if (file.IsSymbolicLink)
                {
                    LogError.BitacoraLog("Symbolic link ignored: " + file.FullName);
                }
                // Si es un directorio, créelo localmente (e ignore .. y. =)
                //. es la carpeta actual
                //.. es la carpeta que está encima de la carpeta actual: la carpeta que contiene la carpeta actual.
                else if (file.Name != "." && file.Name != "..")
                {
                    var destinationFile = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                    // y comience a descargar su contenido de forma recursiva :) en caso de que sea necesario
                    if (recursive)
                    {
                        DescargaArchivosRecursivo(client, file.FullName, destinationFile.FullName, recursive);
                    }
                }
            }
        }

        /// <summary>
        /// Descarga un archivo remoto a través del cliente en un directorio local
        /// </summary>
        /// <param name="client"></param>
        /// <param name="file"></param>
        /// <param name="directory"></param>
        private static void ProcesarArchivoRecursivo(string leyenda, SftpClient client, SftpFile file, string directory)
        {
            var bandera = true;
            try
            {

                LogError.BitacoraLog("Descargando " + leyenda + ", " + file.FullName);
                var rutaLocal = Path.Combine(directory, file.Name);
                if (File.Exists(rutaLocal))
                {
                    File.Delete(rutaLocal);
                }
                using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name)))
                {
                    client.DownloadFile(file.FullName, fileStream);
                }
                Contador++;
            }
            catch (Exception ex)
            {
                bandera = false;
                LogError.BitacoraLog("Error al descargar archivo: " + file.FullName, true, ex);
            }
            var deleteFile = Configuracion.RemoteDelete;
            if (deleteFile && bandera)
            {
                client.DeleteFile(file.FullName);
            }
        }

        /// <summary>
        /// Subir el contenido de un directorio local a un directorio remoto
        /// </summary>
        public static string SubirArchivos()
        {
            var error = "";
            var host = Configuracion.HostName;
            var username = Configuracion.UserName;
            var password = Configuracion.Password;
            var source = Configuracion.LocalDirectory;

            LogError.BitacoraLog("******************************************************************************");
            LogError.BitacoraLog("Comienza carga de archivos");
            LogError.BitacoraLog("******************************************************************************");

            try
            {
                var connectionInfo = new PasswordConnectionInfo(host, Configuracion.Port, username, password);
                using (var sftp = new SftpClient(connectionInfo))
                {
                    sftp.OperationTimeout = TimeSpan.FromSeconds(Configuracion.OperationTimeout);
                    sftp.KeepAliveInterval = sftp.OperationTimeout;
                    sftp.Connect();
                    SubirArchivosRecursivo(source, sftp);
                    LogError.BitacoraLog("******************************************************************************");
                    LogError.BitacoraLog("Termina carga de archivos");
                    LogError.BitacoraLog("******************************************************************************");
                    if (sftp.IsConnected)
                    {
                        sftp.Disconnect();
                    }
                }

            }
            catch (Exception e)
            {
                error = "Se ha detectado una excepción al subir archivos de directorio: " + source + ", Excepción: " + e.ToString();
                LogError.BitacoraLog("******************************************************************************");
                LogError.BitacoraLog(error);
                LogError.BitacoraLog("******************************************************************************");
            }
            return error;
        }

        /// <summary>
        /// Procesar un directorio local de manera recursiva
        /// </summary>
        private static void SubirArchivosRecursivo(string source, SftpClient sftp)
        {
            var files = Directory.GetFiles(source);

            var destination = Configuracion.RemoteDirectory;
            var caracter = destination.Substring(destination.Length - 1);
            if (caracter != "/")
            {
                destination += "/";
            }

            foreach (var file in files)
            {
                var path = Configuracion.LocalDirectory;
                var directory = Path.GetDirectoryName(file);
                directory = directory.Replace(path, "");
                directory = directory.Replace(@"\", "/");

                var destino = destination + directory;
                destino = destino.Replace(@"//", "/");

                if (!sftp.Exists(destino))
                {
                    sftp.CreateDirectory(destino);
                }

                caracter = destino.Substring(destino.Length - 1);
                if (caracter != "/")
                {
                    destino += "/";
                }


                destino += Path.GetFileName(file);

                if (sftp.Exists(destino))
                {
                    sftp.DeleteFile(destino);
                }

                ProcesarArchivo(file, sftp, destino);
            }

            var subdirectorios = Directory.GetDirectories(source);
            foreach (var directory in subdirectorios)
            {
                SubirArchivosRecursivo(directory, sftp);
            }
        }

        /// <summary>
        /// Carga de un archivo local
        /// </summary>
        /// <param name="fileLocal"></param>
        /// <param name="client"></param>
        /// <param name="fileRemote"></param>
        private static void ProcesarArchivo(string fileLocal, SftpClient client, string fileRemote)
        {
            var bandera = true;
            try
            {
                using (var fs = new FileStream(fileLocal, FileMode.Open))
                {
                    client.UploadFile(
                        fs,
                        fileRemote,
                        uploaded =>
                        {
                            LogError.BitacoraLog($"Archivo: {fileLocal}, Porcentaje subido: {(double)uploaded / fs.Length * 100}% of the file.");
                        });
                }
                Contador++;
            }
            catch (Exception ex)
            {
                LogError.BitacoraLog("Error al subir archivo: " + fileLocal, true, ex);
                bandera = false;
            }
            var deleteFile = Configuracion.LocalDelete;
            if (deleteFile && bandera)
            {
                File.Delete(fileLocal);
            }
        }
    }
}
