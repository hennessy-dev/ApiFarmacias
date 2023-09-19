using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private FarmaciaContext _context { get; set; }
        public EmpleadoRepository (FarmaciaContext context) : base (context){
            _context = context;
        }
    }
}