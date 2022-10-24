using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaBD.Transporte
{
    public class EntidadDTO
    {
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public int LocalidadId { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
    }
}
