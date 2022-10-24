using ConsultaBD.Consulta;
using ConsultaBD.Transporte;
using System.Data;

namespace LocalidadesApp.NPOI_Reporte
{
    public class ReporteNativo
    {
        public DataTable DownloadLocalidadesPDF(EntidadDTO modelo, string filename)
        {
            var servicio = new ConsultaCatalogos();
            var listado = servicio.ConsultaListado(modelo);
            DataTable dt = new DataTable("Grid");
            var entidadDetalle = new Registro();

            dt.Columns.AddRange(new DataColumn[10] { 
                AgregarEncabezado(entidadDetalle,"Entidad"),
                AgregarEncabezado(entidadDetalle,"ClaveEntidad"),
                AgregarEncabezado(entidadDetalle,"Municipio"),
                AgregarEncabezado(entidadDetalle,"ClaveMunicipio"),
                AgregarEncabezado(entidadDetalle,"Localidad"),
                AgregarEncabezado(entidadDetalle,"ClaveLocalidad"),
                AgregarEncabezado(entidadDetalle,"PobTotal"),
                AgregarEncabezado(entidadDetalle,"PobMasculina"),
                AgregarEncabezado(entidadDetalle,"PobFemenina"),
                AgregarEncabezado(entidadDetalle,"TotalViviendas")
            });

            foreach (var item in listado)
            {
                dt.Rows.Add(item.Entidad,item.ClaveEntidad,item.Municipio,item.ClaveEntidad,item.Localidad,item.ClaveLocalidad,
                    item.PobTotal,item.PobMasculina,item.PobFemenina,item.TotalViviendas);
            }
            return dt;
        }

        private static DataColumn AgregarEncabezado(Registro entidadDetalle, string columna)
        {
            return new DataColumn(DisplayNameHelper.GetDisplayName(entidadDetalle, columna));
        }
    }
}