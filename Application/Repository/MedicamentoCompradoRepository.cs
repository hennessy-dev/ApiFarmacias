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
    public class MedicamentoCompradoRepository
        : GenericRepository<MedicamentoComprado>,
            IMedicamentoComprado
    {
        private FarmaciaContext _context { get; set; }

        public MedicamentoCompradoRepository(FarmaciaContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicamentoComprado>> GetDrugPurchasedFrom(int ProveedorId)
        {
            var compras = await _context.Compras.Where(c => c.ProveedorId == ProveedorId).ToListAsync();

            List<MedicamentoComprado> medicamentosComprados = new();

            foreach (var compra in compras)
            {
                var medicamentosFiltrados = await _context.MedicamentosComprados
                    .Where(mc => mc.CompraId == compra.Id)
                    .ToListAsync();

                medicamentosComprados.AddRange(medicamentosFiltrados);
            }

            return medicamentosComprados;
        }

        public async Task<double> GetTotalPriceDrugBought()
        {
            var MedicamentosComprados = await _context.MedicamentosComprados.ToListAsync();
            double totalSum = MedicamentosComprados.Sum(mc=>mc.PrecioCompra);
            return totalSum;
        }
    }
}
