namespace Domain.Entities;
public class MedicamentosComprados : BaseEntity
{
    public int CompraId { get; set; }
    public int MedicamentoId { get; set; }
    public int CantidadComprada { get; set; }
    public int PrecioCompra { get; set; }
}