namespace horasApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bandera = true;
            while (bandera)
            {
                var segundosCapturados = capturaInformacion();
                bandera = segundosCapturados != 0; // si segundos==0 bandera=false; en caso contrario
                                                   // bandera=true;

                // un día tiene 86400 segundos
                if (bandera)
                {
                    int segundos = (segundosCapturados % 60);
                    int minutos = (segundosCapturados / 60)%60;
                    int horas = (segundosCapturados / 60 / 60)%24;
                    int dias=segundosCapturados/60/60/24;

                    System.Console.WriteLine("Días: "+dias+", Horas: "+horas+", Minutos: " + minutos + ", Segundos: " + segundos);
                }
            }
        }

        private static int capturaInformacion()
        {
            System.Console.WriteLine("Introduzca los segundos a convertir: ");
            var tiempo=System.Console.ReadLine();
            int.TryParse(tiempo, out int result);
            if (result < 0) result = 0;
            return result;
        }
    }
}
