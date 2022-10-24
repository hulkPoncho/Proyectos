using EmpleadosApp.Transporte;

namespace EmpleadosApp.Negocio
{
    public interface IActualizacionEFC
    {
        List<EmpleadoDTO> ListaEmpleados(int Seleccion);
        void EliminacionFisica(List<int> Elementos);
        void Activacion(List<int> Elementos);

        void EliminacionFisica(string ElementosSeleccionados);
        
        void Activacion(string ElementosSeleccionados);

    }
}
