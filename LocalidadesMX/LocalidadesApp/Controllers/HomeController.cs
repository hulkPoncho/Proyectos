using ClosedXML.Excel;
using ConsultaBD.Consulta;
using ConsultaBD.Transporte;
using LocalidadesApp.NPOI_Reporte;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LocalidadesApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TempData["Ruta_XLS"] = "/Home/DownloadLocalidadesNativo_XLS?";
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
        public JsonResult ConsultaEstados()
        {

            return Json((int)HttpStatusCode.OK);
        }

        [HttpPost]
        public JsonResult ConsultaMunicipios(EntidadDTO modelo)
        {
            var _ConsultaCatalogos = new ConsultaCatalogos();
            var lista = _ConsultaCatalogos.ConsultaMunicipios(modelo).ToList();
            return Json(lista);
        }

        [HttpPost]
        public JsonResult ConsultaLocalidades(EntidadDTO modelo)
        {
            var _ConsultaCatalogos = new ConsultaCatalogos();
            var lista = _ConsultaCatalogos.ConsultaLocalidades(modelo).ToList();
            return Json(lista);
        }


        [HttpPost]
        public JsonResult ConsultaLocalidades1(EntidadDTO modelo)
        {
            var _ConsultaCatalogos = new ConsultaCatalogos();
            var lista = _ConsultaCatalogos.ConsultaLocalidades(modelo).ToList();
            return Json(lista);
        }

        public FileResult DownloadExcel()
        {
            string filename = @"C:\Carga\Grafica.xls";            
            var ejemplo = new EjemploReporte();
            ejemplo.GeneracionReporte(filename);
            var fileBytes = System.IO.File.ReadAllBytes(filename);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        public FileResult DownloadLocalidadesNPOI_XLS(EntidadDTO modelo)
        {
            string filename = @"C:\Carga\Grafica3.xls";

            var reporteExcel = new ReporteNPOI();
            reporteExcel.DownloadLocalidadesPDF(modelo, filename);

            var fileBytes = System.IO.File.ReadAllBytes(filename);
            System.IO.File.Delete(filename);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "ReporteConNPOI.xls");
        }

        public FileResult DownloadLocalidadesClosedXML_XLS(EntidadDTO modelo)
        {
            string filename = @"C:\Carga\Grafica3.xls";

            var reporteExcel = new ReporteClosedXML();
            reporteExcel.DownloadLocalidadesPDF(modelo, filename);

            var fileBytes = System.IO.File.ReadAllBytes(filename);
            System.IO.File.Delete(filename);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "ReporteConClosedXML.xls");
        }

        public FileResult DownloadLocalidadesNativo_XLS(EntidadDTO modelo)
        {
            string filename = @"C:\Carga\Grafica3.xls";

            var reporteExcel = new ReporteNativo();
            var dt=reporteExcel.DownloadLocalidadesPDF(modelo, filename);


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteConDataTable.xlsx");
                }
            }
        }

    }
}