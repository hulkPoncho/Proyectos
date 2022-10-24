using EmpleadosApp.Entidades;
using EmpleadosApp.Models;
using EmpleadosApp.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpleadosApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmpresaContext _Context;
        private readonly IActualizacionEFC _IActualizacionEFC;
        private readonly IActualizacionDapper _IActualizacionDapper;

        public HomeController(ILogger<HomeController> logger,
            EmpresaContext context, IActualizacionEFC actualizacionEFC, 
            IActualizacionDapper iActualizacionDapper)
        {
            _Context = context;
            _IActualizacionEFC = actualizacionEFC;
            _logger = logger;
            _IActualizacionDapper = iActualizacionDapper;
        }

        public JsonResult Actualizacion(string ids, int Seleccion)
        {
            //var lista = new List<int>(Array.ConvertAll(ids.Split(','), Convert.ToInt32));
            if (Seleccion == -1)
            {
                _IActualizacionDapper.EliminacionFisica(ids);
            }
            else
            {
                _IActualizacionDapper.Activacion(ids);
            }

            return Json((int)StatusCodes.Status200OK);
        }

        public IActionResult Lista(int Seleccion = -1)
        {
            var lista = _IActualizacionEFC.ListaEmpleados(Seleccion);
            return PartialView(lista);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}