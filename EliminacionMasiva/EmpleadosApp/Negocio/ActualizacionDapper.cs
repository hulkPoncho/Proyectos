using EmpleadosApp.Entidades;
using Dapper;

namespace EmpleadosApp.Negocio
{
    public class ActualizacionDapper : IActualizacionDapper
    {
        private readonly DapperContext _context;

        public ActualizacionDapper(DapperContext context)
        {
            _context = context;
        }

        public void Activacion(string ElementosSeleccionados)
        {
            var queryString = "UPDATE dbo.Empleado SET EsActivo=EsActivo ^ 1 where EmpleadoId in ("
                + ElementosSeleccionados + ")";
            var result = EjecucionQueryString(queryString);
        }

        public void EliminacionFisica(string ElementosSeleccionados)
        {
            var queryString = "DELETE FROM dbo.Empleado where EmpleadoId in (" + 
                ElementosSeleccionados + ")";
            var result = EjecucionQueryString(queryString);
        }

        private int EjecucionQueryString(string queryString)
        {
            int result;
            using (var connection = _context.CreateConnection())
            {
                result = connection.Execute(queryString);
            }

            return result;
        }

    }
}
