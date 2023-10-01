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
            var MedicamentosVendidos = await _context.MedicamentosVendidos.Include(m => m.Medicamento)
                .Where(m => m.Medicamento.Nombre == drugName)
                .ToListAsync();
            var VentasIds = MedicamentosVendidos.Select(mv => mv.VentaId).ToList();

            var Pacientes = _context.Pacientes.Where(p => VentasIds.Contains(p.Id)).ToList();
            return Pacientes;
        }
    }
}
