using Domain.Entities;

namespace Domain.Interfaces;

public interface IProducto : IGenericRepository<Producto>
{
    Task<IEnumerable<Producto>> GetProductosMasCaros(int cantidad);
}
