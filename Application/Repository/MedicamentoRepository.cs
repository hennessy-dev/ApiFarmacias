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
    }
}