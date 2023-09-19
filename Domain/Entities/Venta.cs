namespace Domain.Entities;
public class Venta : BaseEntity
{
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; }
    public int EmpleadoId { get; set; }
    public Empleado Empleado { get; set; }
    public DateTime FechaVenta { get; set; }
    public ICollection<MedicamentoVendido> MedicamentosVendidos { get; set; }
}