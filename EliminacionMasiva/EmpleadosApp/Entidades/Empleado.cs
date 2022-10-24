using System;
using System.Collections.Generic;

namespace EmpleadosApp.Entidades
{
    public partial class Empleado
    {
        public int EmpleadoId { get; set; }
        public string? Nombre { get; set; }
        public bool? EsActivo { get; set; }
    }
}
