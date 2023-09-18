namespace Domain.Entities;
public class Medicamentos : BaseEntity
{
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public int ProovedorId { get; set; }
}