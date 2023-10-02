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

       public async Task<Empleado> EmployeeWhoSoldMoreKindOfDrugsBetween(DateTime firstDate, DateTime lastDate)
        {
            var employee = await _context.Empleados
                .Include(e => e.Ventas)
                .ThenInclude(v => v.MedicamentosVendidos)
                .Where(e => e.Ventas.Any(v => v.FechaVenta >= firstDate && v.FechaVenta <= lastDate))
                .OrderByDescending(e => e.Ventas
                    .SelectMany(v => v.MedicamentosVendidos)
                    .Select(mv => mv.MedicamentoId)
                    .Distinct()
                    .Count())
                .FirstOrDefaultAsync();

            return employee;
        }

        public async Task<List<Empleado>> EmployeesWhoDidntSellBetween(DateTime firtsDate, DateTime lastDate)
        {
            var employees = await _context.Empleados.Include(m=>m.Ventas.Where(v=>v.FechaVenta >= firtsDate && v.FechaVenta <= lastDate))
            .Where(e => e.Ventas.Count() == 0).ToListAsync();
            return employees;
        }
    }
}