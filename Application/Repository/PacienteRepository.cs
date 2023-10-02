using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class PacienteRepository : GenericRepository<Paciente>, IPaciente
    {
        private FarmaciaContext _context { get; set; }

        public PacienteRepository(FarmaciaContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> PatientsWhoHavePurchased(string drugName)
        {
            var MedicamentosVendidos = await _context.MedicamentosVendidos
                .Include(m => m.Medicamento)
                .Where(m => m.Medicamento.Nombre == drugName)
                .ToListAsync();
            var VentasIds = MedicamentosVendidos.Select(mv => mv.VentaId).ToList();

            var Pacientes = _context.Pacientes.Where(p => VentasIds.Contains(p.Id)).ToList();
            return Pacientes;
        }

        public async Task<(Paciente p, double total)> PatientWhoSpentMostMoney()
        {
            var medicamentosVendidosQuery = await _context.MedicamentosVendidos
                .GroupBy(mv => mv.Venta.PacienteId)
                .Select(g => new { PacienteId = g.Key, TotalGastado = g.Sum(mv => mv.Precio) })
                .OrderByDescending(g => g.TotalGastado)
                .FirstOrDefaultAsync();

            var paciente = await _context.Pacientes
                .Include(p => p.Ventas)
                .FirstOrDefaultAsync(p => p.Id == medicamentosVendidosQuery.PacienteId);

            var totalGastado = medicamentosVendidosQuery.TotalGastado;

            return (paciente, totalGastado);
        }

        public async Task<IEnumerable<Paciente>> PatientsWhoBoughtInLastYear(string drugName)
        {
            var currentDate = DateTime.Now;
            var oneYearAgo = currentDate.AddYears(-1);

            var pacientes = await _context.MedicamentosVendidos
                .Include(mv => mv.Venta.Paciente)
                .Include(mv => mv.Medicamento)
                .Where(
                    mv =>
                        mv.Venta.FechaVenta >= oneYearAgo
                        && mv.Venta.FechaVenta <= currentDate
                        && mv.Medicamento.Nombre.ToLower() == drugName.ToLower()
                )
                .Select(mv => mv.Venta.Paciente)
                .Distinct()
                .ToListAsync();

            return pacientes;
        }

        public async Task<IEnumerable<Paciente>> PatientsWhoHaventBoughtAnythingBetween(DateTime initialDate, DateTime lastDate)
        {
            var Pacientes = await _context.Pacientes.Include(p=> p.Ventas)
            .Where(p=> !p.Ventas.Any(v=> v.FechaVenta >= initialDate && v.FechaVenta <= lastDate ))
            .ToListAsync();
            return Pacientes;
        }
    }
}
