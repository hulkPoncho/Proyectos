using ConsultaBD.Consulta;
using ConsultaBD.Transporte;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;

namespace LocalidadesApp.NPOI_Reporte
{
    public class ReporteNPOI
    {
        public void DownloadLocalidadesPDF(EntidadDTO modelo, string filename)
        {
            var servicio = new ConsultaCatalogos();
            using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {

                IWorkbook workbook = new XSSFWorkbook();

                var sheet = workbook.CreateSheet("Sheet1");
                ICellStyle style1 = CrearEstiloTitulo(workbook);

                var renglon = 0;
                var row = CreaRenglon(sheet, renglon);
                Encabezados(style1, row);

                // Estilo 1
                ICellStyle estiloGenerico1 = ObtenerEstilo1(workbook);

                // Estilo 2
                ICellStyle estiloGenerico2 = ObtenerEstilo2(workbook);

                var listado = servicio.ConsultaListado(modelo);
                renglon = ConstruccionCuerpoExcel(sheet, ref row, estiloGenerico1, estiloGenerico2, listado);

                workbook.Write(fs);
                workbook.Close();
            }
        }

        private ICellStyle ObtenerEstilo1(IWorkbook workbook)
        {
            var estiloGenerico = workbook.CreateCellStyle();
            estiloGenerico.FillPattern = FillPattern.SolidForeground;
            estiloGenerico.BorderBottom = BorderStyle.Medium;
            estiloGenerico.BorderLeft = BorderStyle.Medium;
            estiloGenerico.BorderRight = BorderStyle.Medium;
            estiloGenerico.BorderTop = BorderStyle.Medium;
            estiloGenerico.FillForegroundColor = HSSFColor.LightYellow.Index;
            return estiloGenerico;
        }

        private ICellStyle ObtenerEstilo2(IWorkbook workbook)
        {
            var estiloGenerico = workbook.CreateCellStyle();
            estiloGenerico.FillPattern = FillPattern.SolidForeground;
            estiloGenerico.BorderBottom = BorderStyle.Medium;
            estiloGenerico.BorderLeft = BorderStyle.Medium;
            estiloGenerico.BorderRight = BorderStyle.Medium;
            estiloGenerico.BorderTop = BorderStyle.Medium;
            estiloGenerico.FillForegroundColor = HSSFColor.White.Index;
            return estiloGenerico;
        }

        private int ConstruccionCuerpoExcel(
            ISheet sheet, ref IRow row, ICellStyle style1, ICellStyle style2, List<Registro> listado)
        {
            int renglon = 1;
            foreach (var item in listado)
            {
                row = CreaRenglon(sheet, renglon);

                ICellStyle estilo = null;
                if (renglon % 2 == 1)
                    estilo=style1;
                else
                    estilo=style2;

                PesonalizaCelda(estilo, item.Entidad, row, 0);
                PesonalizaCelda(estilo, item.ClaveEntidad, row, 1);
                PesonalizaCelda(estilo, item.Municipio, row, 2);
                PesonalizaCelda(estilo, item.ClaveMunicipio, row, 3);
                PesonalizaCelda(estilo, item.Localidad, row, 4);
                PesonalizaCelda(estilo, item.ClaveLocalidad, row, 5);
                PesonalizaCelda(estilo, item.PobTotal, row, 6);
                PesonalizaCelda(estilo, item.PobMasculina, row, 7);
                PesonalizaCelda(estilo, item.PobFemenina, row, 8);
                PesonalizaCelda(estilo, item.TotalViviendas, row, 9);
                renglon++;
            }

            return renglon;
        }

        private void Encabezados(ICellStyle style, IRow row)
        {
            var entidadDetalle = new Registro();
            InsercionEncabezadoColumna(style, row, entidadDetalle, "Entidad",0);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "ClaveEntidad", 1);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "Municipio", 2);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "ClaveMunicipio", 3);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "Localidad", 4);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "ClaveLocalidad", 5);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "PobTotal", 6);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "PobMasculina", 7);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "PobFemenina", 8);
            InsercionEncabezadoColumna(style, row, entidadDetalle, "TotalViviendas", 9);
        }

        private void InsercionEncabezadoColumna(ICellStyle style, IRow row, Registro entidadDetalle, string columna, int indice)
        {
            var llave = DisplayNameHelper.GetDisplayName(entidadDetalle, columna);
            PesonalizaCelda(style, llave, row, indice);
        }

        private ICellStyle CrearEstiloTitulo(IWorkbook workbook)
        {
            var style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = HSSFColor.Black.Index;
            style1.FillPattern = FillPattern.SolidForeground;

            IFont fontTitulo = workbook.CreateFont();
            fontTitulo.Color = IndexedColors.White.Index;
            fontTitulo.FontHeightInPoints = (short)10;
            fontTitulo.FontName = "Arial";
            fontTitulo.IsBold = false;
            fontTitulo.IsItalic = false;
            style1.SetFont(fontTitulo);
            return style1;
        }

        private void PesonalizaCelda(
            ICellStyle style, object valor, IRow renglon, int columna)
        {
            var cell = renglon.CreateCell(columna);
            cell.CellStyle = style;
            var valorObjeto = valor == null ? "" : valor.ToString();
            cell.SetCellValue(valorObjeto);
        }

        private IRow CreaRenglon(ISheet sheet, int renglon)
        {
            return sheet.CreateRow(renglon);
        }
    }
}