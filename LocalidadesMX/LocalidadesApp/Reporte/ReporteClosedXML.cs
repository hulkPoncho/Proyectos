using ClosedXML.Excel;
using ConsultaBD.Consulta;
using ConsultaBD.Transporte;
using System.Collections.Generic;
using System.IO;

namespace LocalidadesApp.NPOI_Reporte
{
    public class ReporteClosedXML
    {
        public void DownloadLocalidadesPDF(EntidadDTO modelo, string filename)
        {
            var servicio = new ConsultaCatalogos();
            using (var woorkbook = new XLWorkbook())
            {

                var woorksheet = woorkbook.Worksheets.Add("Sheet1");

                var renglon = 0;
                Encabezados(woorksheet);

                RangoTipo(woorksheet,"A1","J1", XLColor.White, XLColor.Black);

                var listado = servicio.ConsultaListado(modelo);
                renglon = ConstruccionCuerpoExcel(woorksheet, listado);

                using (var stream = new MemoryStream())
                {
                    woorkbook.SaveAs(stream);
                    var file = new FileStream(filename, FileMode.Create, FileAccess.Write);
                    stream.WriteTo(file);
                    file.Close();
                }
            }
        }

        private static void RangoTipo(IXLWorksheet woorksheet, string columnaInicio, string columnaFin,
            XLColor color, XLColor fondo)
        {
            var rango = woorksheet.Range(columnaInicio+":"+columnaFin); //Seleccionamos un rango
            rango.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thick); //Generamos las lineas exteriores
            rango.Style.Border.SetInsideBorder(XLBorderStyleValues.Medium); //Generamos las lineas interiores
            rango.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; //Alineamos horizontalmente
            rango.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;  //Alineamos verticalmente
            rango.Style.Font.FontSize = 14; //Indicamos el tamaño de la fuente
            rango.Style.Fill.BackgroundColor = fondo;//XLColor.AliceBlue;
            rango.Style.Font.FontColor = color;
        }

        private int ConstruccionCuerpoExcel(
            IXLWorksheet woorksheet, List<Registro> listado)
        {
            int renglon = 2;
            foreach (var item in listado)
            {
                woorksheet.Cell(renglon, 1).Value = item.Entidad;
                woorksheet.Cell(renglon, 2).Value = item.ClaveEntidad;
                woorksheet.Cell(renglon, 3).Value = item.Municipio;
                woorksheet.Cell(renglon, 4).Value = item.ClaveMunicipio;
                woorksheet.Cell(renglon, 5).Value = item.Localidad;
                woorksheet.Cell(renglon, 6).Value = item.ClaveLocalidad;
                woorksheet.Cell(renglon, 7).Value = item.PobTotal;
                woorksheet.Cell(renglon, 8).Value = item.PobMasculina;
                woorksheet.Cell(renglon, 9).Value = item.PobFemenina;
                woorksheet.Cell(renglon, 10).Value = item.TotalViviendas;

                var fondo = renglon % 2 == 1 ? XLColor.Maroon : XLColor.White;
                var color = renglon % 2 == 1 ? XLColor.White : XLColor.Black;
                RangoTipo(woorksheet, "A"+renglon, "J"+renglon, color, fondo);
                renglon++;
            }

            return renglon;
        }

        private void Encabezados(IXLWorksheet woorksheet)
        {
            var entidadDetalle = new Registro();
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "Entidad",1);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "ClaveEntidad", 2);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "Municipio", 3);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "ClaveMunicipio", 4);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "Localidad", 5);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "ClaveLocalidad", 6);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "PobTotal", 7);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "PobMasculina", 8);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "PobFemenina", 9);
            InsercionEncabezadoColumna(woorksheet, entidadDetalle, "TotalViviendas", 10);
        }

        private void InsercionEncabezadoColumna(IXLWorksheet woorksheet, Registro entidadDetalle, string columna, int indice)
        {
            var llave = DisplayNameHelper.GetDisplayName(entidadDetalle, columna);
            woorksheet.Cell(1, indice).Value = llave;
        }
    }
}