using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Application.Repository
{
    public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
    {
        private FarmaciaContext _context { get; set; }

        public MedicamentoRepository(FarmaciaContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicamento>> GetLessThan50()
        {
            var Medicamentos = await _context.Medicamentos.Where(m => m.Stock < 50).ToListAsync();
            return Medicamentos;
        }

        public async Task<IEnumerable<Medicamento>> GetMedicamentoProveedor()
        {
            var listMedicamentos = await _context.Medicamentos
                .Include(m => m.Proveedor)
                .ToListAsync();
            return listMedicamentos;
        }

        public async Task UpdateStock(int medicamentoId, int cantidadVendida)
        {
            try
            {
                Medicamento medicamento = await _context.Medicamentos.FirstOrDefaultAsync(
                    m => m.Id == medicamentoId
                );
                medicamento.Stock -= cantidadVendida;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Medicamento no encontrado");
            }
        }

        public async Task<IEnumerable<Medicamento>> GetDrugExpiresBefore(DateTime baseDate)
        {
            var medicamentos = await _context.Medicamentos
                .Where(m => DateTime.Compare(m.FechaExpiracion, baseDate) <= 0)
                .ToListAsync();
            return medicamentos;
        }

        public async Task<IEnumerable<Medicamento>> GetUnsoldDrug()
        {
            var MedicamentosVendidos = await _context.MedicamentosVendidos.ToListAsync();
            var Medicamentos = await _context.Medicamentos.ToListAsync();
            var MedicamentosFiltrados = Medicamentos.Where(
                m => !MedicamentosVendidos.Any(mv => mv.MedicamentoId == m.Id)
            );
            return MedicamentosFiltrados;
        }

        public async Task<Medicamento> GetMostExpensiveDrug()
        {
            var medicamento = await _context.Medicamentos
                .OrderByDescending(m => m.Precio)
                .FirstOrDefaultAsync();
            return medicamento;
        }

        public async Task<Medicamento> GetLeastSoldDrug()
        {
            var medicamentosVendidos = await _context.MedicamentosVendidos.ToListAsync();
            var medicamentosVendidosAgrupados = medicamentosVendidos
                .GroupBy(mv => mv.MedicamentoId)
                .ToList();
            var gruposOrdenados = medicamentosVendidosAgrupados.OrderBy(
                g => g.Sum(mv => mv.CantidadVendida)
            );
            var grupoMenosVendido = gruposOrdenados.FirstOrDefault();
            var MedicamentoMenosVendido = await _context.Medicamentos.FirstOrDefaultAsync(
                m => m.Id == grupoMenosVendido.Key
            );
            return MedicamentoMenosVendido;
        }

        public async Task<(List<Medicamento> medicamentos, List<int> totales)> GetTotalDrugSoldPer(
            DateTime initialDate,
            DateTime lastDate
        )
        {
            var medicamentosQuery = await _context.MedicamentosVendidos
                .Include(v => v.Medicamento)
                .Include(v => v.Venta)
                .Where(v => v.Venta.FechaVenta >= initialDate && v.Venta.FechaVenta <= lastDate)
                .GroupBy(v => v.Medicamento)
                .Select(g => new { Medicamento = g.Key, Totales = g.Sum(mv => mv.CantidadVendida) })
                .ToListAsync();

            var medicamentos = medicamentosQuery.Select(m => m.Medicamento).ToList();
            var totales = medicamentosQuery.Select(m => m.Totales).ToList();

            return (medicamentos, totales);
        }

        public async Task<List<Medicamento>> GetTotalDrugUnsoldBetween(
            DateTime initialDate,
            DateTime lastDate
        )
        {
            var medicamentos =await _context.Medicamentos.Include(M=>M.MedicamentosVendidos).ThenInclude(v=>v.Venta)
            .Where(m => !m.MedicamentosVendidos.Any(m=> m.Venta.FechaVenta >= initialDate && m.Venta.FechaVenta <= lastDate) && m.MedicamentosVendidos.Count() == 0)
            .ToListAsync();
            return medicamentos;

        }

        public async Task<List<Medicamento>> GetDrugWithPiceMoreThanAndStokLeastThan(double price, int stok)
        {
            var medicamentos = await _context.Medicamentos.Where(m => m.Precio > price && m.Stock < stok).ToListAsync();
            return medicamentos;
        }
    }
}
