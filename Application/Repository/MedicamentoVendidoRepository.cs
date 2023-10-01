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
            double totalSum = MedicamentosVendidos.Sum(mv => mv.Precio);
            return totalSum;
        }

        public async Task<IEnumerable<MedicamentoVendido>> GetDrugSoldAfterAndBeforeThan(
            DateTime firstDate,
            DateTime lastDate
        )
        {
            var ventasEnRango = await _context.Ventas
                .Where(v => firstDate <= v.FechaVenta && v.FechaVenta <= lastDate)
                .ToListAsync();
            var idsVentasEnRango = ventasEnRango.Select(v => v.Id).ToList();
            var medicamentosVendidos = _context.MedicamentosVendidos
                .Where(mv => idsVentasEnRango.Contains(mv.VentaId))
                .ToList();

            return medicamentosVendidos;
        }

        public async Task<double> AverageDrugSoldPerSale()
        {
            var PromedioPorVenta =await _context.MedicamentosVendidos.GroupBy(mv => mv.VentaId).Select(grupo => new{
                VentaId = grupo.Key,
                PromedioCantidadVendida = grupo.Average(mv => mv.CantidadVendida)
            })
            .ToListAsync();
            var promedioTotal = PromedioPorVenta.Average(g=>g.PromedioCantidadVendida);
            return promedioTotal;
        }
    }
}
