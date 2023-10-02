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
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private FarmaciaContext _context { get; set; }
        public EmpleadoRepository (FarmaciaContext context) : base (context){
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> EmployeesWhoHaventMadeSales()
        {
            var Empleados = await _context.Empleados.Where(e=>e.Ventas.Count() == 0).ToListAsync();
            return Empleados;
        }

        public async Task<IEnumerable<Empleado>> EmployeesWhoHaveLessThan5Sales()
        {
            var Empleados = await _context.Empleados.Where(e=>e.Ventas.Count() < 5).ToListAsync();
            return Empleados;
        }
    }
}