namespace Domain.Entities;
public class Compra : BaseEntity
{
    public DateTime FechaCompra { get; set; }
    public int ProovedorId { get; set; }
}