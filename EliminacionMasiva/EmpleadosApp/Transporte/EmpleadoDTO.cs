using System.ComponentModel.DataAnnotations;

namespace EmpleadosApp.Transporte
{
    public class EmpleadoDTO
    {
        [Required]
        [Display(Name = "Empleado ID")]
        public int EmpleadoId { get; set; }

        [Required]
        [Display(Name = "Nombre del empleado")]

        public string? Nombre { get; set; }
        [Required]
        [Display(Name = "Es activo")]

        public bool? EsActivo { get; set; }
    }
}
