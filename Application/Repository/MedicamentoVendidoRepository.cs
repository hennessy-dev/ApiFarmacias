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
    public class MedicamentoVendidoRepository
        : GenericRepository<MedicamentoVendido>,
            IMedicamentoVendido
    {
        private FarmaciaContext _context { get; set; }

        public MedicamentoVendidoRepository(FarmaciaContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicamentoVendido>> GetDrugsSoldAfter(
            IEnumerable<Venta> ventas
        )
        {
            var MedicamentosVendidos = await _context.MedicamentosVendidos.ToListAsync();
            var MedicamentosFiltrados = MedicamentosVendidos.Where(
                mv => ventas.Any(v => mv.VentaId == v.Id)
            );
            return MedicamentosFiltrados;
        }

        public async Task<int> GetTotalDrugsSold(string drugName)
        {
            var medicamentosVendidos = _context.MedicamentosVendidos
                .Include(mv => mv.Medicamento)
                .Where(mv => mv.Medicamento.Nombre.ToLower() == drugName.ToLower());
            int total = await medicamentosVendidos.SumAsync(mv => mv.CantidadVendida);
            return total;
        }

        public async Task<double> GetTotalPriceDrugSold()
        {
            var MedicamentosVendidos = await _context.MedicamentosVendidos.ToListAsync();
            double totalSum = MedicamentosVendidos.Sum(mv=>mv.Precio);
            return totalSum;
        }
    }
}
