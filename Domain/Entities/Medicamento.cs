namespace Domain.Entities;
public class Medicamento : BaseEntity
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public int ProovedorId { get; set; }
    public Proveedor Proveedor { get; set; }
    public ICollection<MedicamentoVendido> MedicamentosVendidos { get; set; }
    public ICollection<MedicamentoComprado> MedicamentosComprados { get; set; }
}