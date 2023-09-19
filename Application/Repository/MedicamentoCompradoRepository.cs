using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class MedicamentoCompradoRepository : GenericRepository<MedicamentoComprado>, IMedicamentoComprado
    {
        private FarmaciaContext _context { get; set; }
        public MedicamentoCompradoRepository (FarmaciaContext context) : base (context){
            _context = context;
        }
    }
}