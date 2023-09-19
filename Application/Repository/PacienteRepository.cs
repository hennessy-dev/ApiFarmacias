using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class PacienteRepository: GenericRepository<Paciente>, IPaciente
    {
        private FarmaciaContext _context { get; set; }
        public PacienteRepository (FarmaciaContext context) : base (context){
            _context = context;
        }
    }
}