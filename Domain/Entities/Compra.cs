namespace Domain.Entities;
public class Compra : BaseEntity
{
    public DateTime FechaCompra { get; set; }
    public int ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; }
    public ICollection<MedicamentoComprado> MedicamentosComprados { get; set; }
}