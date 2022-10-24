using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consulta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lineas = System.IO.File.ReadAllLines(@"C:\Users\flord\Downloads\Aplicaciones con ASP .NET 5 MVC, D3, SQL Server, .NET 6_2022-08-02_03-31-35.csv");
            var listadoNet5 = (from a in lineas select a.Split(',')[0]).ToList();
            listadoNet5 = listadoNet5.Skip(1).ToList();

            lineas = System.IO.File.ReadAllLines(@"C:\Users\flord\Downloads\CRUD en ASP .Net 6 MVC_2022-08-02_03-24-29.csv");
            var listadoNet6 = (from a in lineas select a.Split(',')[0].Trim()).ToList();
            listadoNet6 = listadoNet6.Skip(1).ToList();

            lineas = System.IO.File.ReadAllLines(@"C:\Users\flord\Downloads\SQL Joins con Entity Framework Core y Dapper en MVC .Net 5_2022-08-02_03-28-33.csv");
            var listadoSqlJoins = (from a in lineas select a.Split(',')[0]).ToList();
            listadoSqlJoins = listadoSqlJoins.Skip(1).ToList();

            var elementos = listadoNet5.Concat(listadoNet6).Concat(listadoSqlJoins).ToList();

            var unicos = elementos.GroupBy(g => new { g }).Select(g => g.First()).ToList()
                .Select(s => s).ToList();

            var repetidos = elementos.GroupBy(x => x).Where(x => x.Count() > 1)
                .Select(s => s.Key).Cast<String>().ToArray();

            var cantidad = (from a in listadoNet6 where listadoNet5.Contains(a) select a).ToList();
            cantidad = (from a in listadoSqlJoins where listadoNet5.Contains(a) select a).ToList();
        }
    }
}
