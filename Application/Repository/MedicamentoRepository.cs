using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Persistence;

namespace Application.Repository
{
    public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
    {
        private FarmaciaContext _context { get; set; }
        public MedicamentoRepository (FarmaciaContext context) : base (context){
            _context = context;
        }
        public async Task<IEnumerable<Medicamento>> GetLessThan50 (){
            var Medicamentos = await _context.Medicamentos.Where(m=>m.Stock < 50).ToListAsync();
            return Medicamentos; 
        }
        public async Task<IEnumerable<Medicamento>> GetMedicamentoProveedor ()  {
            var listMedicamentos = await _context.Medicamentos.Include(m => m.Proveedor).ToListAsync();
            return listMedicamentos;
        }

        public async Task UpdateStock (int medicamentoId,int cantidadVendida){
            try{
                Medicamento medicamento =await _context.Medicamentos.FirstOrDefaultAsync(m=>m.Id == medicamentoId);
                medicamento.Stock -= cantidadVendida;
                await _context.SaveChangesAsync();
            } catch {
                throw new InvalidOperationException("Medicamento no encontrado");
            }
        }

        public async Task<IEnumerable<Medicamento>> GetDrugExpiresBefore(DateTime baseDate)
        {
            var medicamentos = await _context.Medicamentos.Where(m=> DateTime.Compare(m.FechaExpiracion, baseDate) <= 0).ToListAsync();
            return medicamentos;
        }
    }
}