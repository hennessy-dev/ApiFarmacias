namespace Domain.Entities;
public class MedicamentoVendido : BaseEntity
{
    public int VentaId { get; set; }
    public Venta Venta { get; set; }
    public int MedicamentoId { get; set; }
    public Medicamento Medicamento { get; set; }
    public int CantidadVendida { get; set; }
    public double Precio { get; set; }
}