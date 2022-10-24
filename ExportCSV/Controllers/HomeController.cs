using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ExportApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public FileResult Export(string empleado)
        {
            List<object> empleados = ConsultaEmpleados(empleado);
            StringBuilder sb = ContruccionArchivoCSV(empleados);
            byte[] isoBytes = ManejoEncoding(sb);

            return File(isoBytes, "text/csv", "ReporteEmpleados.csv");

            //return File(new System.Text.UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "ReporteEmpleados.csv");
        }

        private static byte[] ManejoEncoding(StringBuilder sb)
        {
            // Manejo del encoding con ISO-8859-1
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Encoding utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(sb.ToString());
            byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            return isoBytes;
        }

        private static StringBuilder ContruccionArchivoCSV(List<object> empleados)
        {
            // construcción del archivo csv
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < empleados.Count; i++)
            {
                string[] customer = (string[])empleados[i];
                for (int j = 0; j < customer.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(customer[j] + ',');
                }
                //Append new line character.
                sb.Append("\r\n");
            }

            return sb;
        }

        private static List<object> ConsultaEmpleados(string empleado)
        {
            ConsultasEntities entities = new ConsultasEntities();

            // consulta a BD
            var empleados = (from _empleados in entities.Empleado.ToList()
                             where _empleados.Nombre.Contains(empleado)
                             select new[] {
                                 _empleados.Nombre,
                                 _empleados.Departamento == null ? "" : _empleados.Departamento.Nombre
                             }).ToList<object>();

            empleados.Insert(0, new string[2] { "Nombre empleado", "Departamento" });
            return empleados;
        }
    }
}
