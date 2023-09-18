namespace Domain.Entities;
public class Venta : BaseEntity
{
    public int PacienteId { get; set; }
    public int EmpleadoId { get; set; }
    public DateTime FechaVenta { get; set; }
}