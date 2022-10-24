using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inyeccion.Validaciones
{
    public class LoginValidateEntity
    {
        public bool ValidacionCredenciales(string username, string pwd)
        {
            var conteo = 0;
            string sql =
                $"SELECT COUNT(*) AS Conteo from Empleado WHERE UserName='{username}'" +
                $" AND Pwd='{pwd}'";

            using (var ctx = new NominaEntities())
            {
                conteo = ctx.Database
                                    .SqlQuery<int>(sql).First();
            }
            return conteo > 0;
        }

        public bool ValidacionCredencialesCount(string username, string pwd)
        {
            var conteo = 0;

            using (var ctx = new NominaEntities())
            {
                conteo = ctx.Empleado.Where(s => s.UserName == username && s.Pwd == pwd).Count();
            }
            return conteo > 0;
        }


    }
}
