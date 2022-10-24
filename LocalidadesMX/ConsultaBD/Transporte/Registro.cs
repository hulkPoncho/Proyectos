using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaBD.Transporte
{
    public class Registro:EntidadDetalleDTO
    {
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }
        [Display(Name = "Clave del Municipio")]
        public string ClaveMunicipio { get; set; }
        [Display(Name = "Localidad")]
        public string Localidad { get; set; }
        [Display(Name = "Clave de localidad")]
        public string ClaveLocalidad{ get; set; }
        [Display(Name = "Entidad")]
        public string Entidad { get; set; }
        [Display(Name = "Clave de entidad")]
        public string ClaveEntidad { get; set; }
    }
}
