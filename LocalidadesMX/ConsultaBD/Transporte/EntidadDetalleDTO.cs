using System.ComponentModel.DataAnnotations;

namespace ConsultaBD.Transporte
{
    public class EntidadDetalleDTO
    {
        [Display(Name = "Población Femenina")]
        public int PobFemenina { get; set; }
        [Display(Name = "Población Masculina")]
        public int PobMasculina { get; set; }
        [Display(Name = "Población total")]
        public int PobTotal { get; set; }
        [Display(Name = "Total de viviendas")]
        public int TotalViviendas { get; set; }
    }
}
