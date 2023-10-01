using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
    {
        private FarmaciaContext _context { get; set; }

        public ProveedorRepository(FarmaciaContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> ProvidersWhoHaveNotSoldInLastYear()
        {
            var UltimoAño = DateTime.Now.AddYears(-1);

            var proveedoresFiltrados = await _context.Proveedores
                .Where(
                    proveedor =>
                        !_context.Compras.Any(
                            compra =>
                                compra.ProveedorId == proveedor.Id
                                && compra.FechaCompra >= UltimoAño
                        )
                )
                .ToListAsync();
            return proveedoresFiltrados;
        }
    }
}
