using EmpleadosApp.Entidades;
using EmpleadosApp.Transporte;
using Microsoft.EntityFrameworkCore;


namespace EmpleadosApp.Negocio
{
    public class ActualizacionEFC : IActualizacionEFC
    {
        private readonly EmpresaContext _context;
        public ActualizacionEFC(EmpresaContext context)
        {
            _context = context;
        }

        public void EliminacionFisica(List<int> elementos)
        {
            var empleados = _context.Empleados.
                Where(s => elementos.Contains(s.EmpleadoId)).Select(s => s).ToList();

            //foreach(var empleado in empleados)
            //{
            //    _context.Empleados.Remove(empleado);
            //}

            _context.Empleados.RemoveRange(empleados);
            _context.SaveChanges();
        }

        public void Activacion(List<int> elementos)
        {
            var empleados = _context.Empleados.Where(s => elementos.Contains(s.EmpleadoId)).
                Select(s => s).ToList();
            foreach(var empleado in empleados)
            {
                empleado.EsActivo = !empleado.EsActivo;
            }
            _context.SaveChanges();
        }

        public List<EmpleadoDTO> ListaEmpleados(int Seleccion)
        {
            bool? EsActivo = (Seleccion == -1) ? null : (Seleccion != 1);
            var Empleados = (from a in _context.Empleados
                             where EsActivo == null ? true :
                                (a.EsActivo == (bool)EsActivo)
                             select new EmpleadoDTO
                             {
                                 EmpleadoId = a.EmpleadoId,
                                 Nombre = a.Nombre,
                                 EsActivo = a.EsActivo
                             }).ToList();
            return Empleados;
        }

        public void EliminacionFisica(string ElementosSeleccionados)
        {
            var queryString = "DELETE FROM dbo.Empleado where EmpleadoId in (" + 
                ElementosSeleccionados + ")";
            var resultado = _context.Database.ExecuteSqlRaw(queryString);
        }

        public void Activacion(string ElementosSeleccionados)
        {
            var queryString = "UPDATE dbo.Empleado SET EsActivo=EsActivo ^ 1 where EmpleadoId in ("
                + ElementosSeleccionados + ")";
            var resultado = _context.Database.ExecuteSqlRaw(queryString);
        }

    }
}
