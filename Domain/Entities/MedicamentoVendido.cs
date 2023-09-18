namespace Domain.Entities;
public class MedicamentoVendido : BaseEntity
{
    public int VentaId { get; set; }
    public int MedicamentoId { get; set; }
    public int CantidadVendida { get; set; }
    public int Precio { get; set; }
}