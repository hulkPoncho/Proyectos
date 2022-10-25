using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inyeccion
{
    public class LoginValidate
    {
        public bool ValidacionCredenciales(string username, string pwd)
        {
            string sql =
            $"SELECT COUNT(*) AS Conteo from Empleado WHERE UserName='{username}' AND Pwd='{pwd}'";
            var conexion = ConfigurationManager.ConnectionStrings["strConn"].ToString();
            var conteo = 0;
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //var salaryParam = new SqlParameter("salary", SqlDbType.Money);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            conteo = int.Parse(reader[0].ToString());
                        }
                    }
                }
            }
            return conteo>0;
        }

        public bool ValidacionCredencialesCorrecto(string username, string pwd)
        {
            string sql =
            $"SELECT COUNT(*) AS Conteo from Empleado WHERE UserName=@UserName AND Pwd=@Pwd";
            var conexion = ConfigurationManager.ConnectionStrings["strConn"].ToString();
            var conteo = 0;
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Pwd", pwd);

                    //var salaryParam = new SqlParameter("salary", SqlDbType.Money);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            conteo = int.Parse(reader[0].ToString());
                        }
                    }
                }
            }
            return conteo > 0;
        }

    }
}
