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
    public class VentaRepository : GenericRepository<Venta>, IVenta
    {
        private FarmaciaContext _context { get; set; }
        public VentaRepository (FarmaciaContext context) : base (context){
            _context = context;
        }
        public async Task<IEnumerable<Venta>> GetSalesAfter(DateTime dateBase){
            var ventasFiltradas = await _context.Ventas.Where(v => DateTime.Compare(v.FechaVenta, dateBase) > 0).ToListAsync();
            return ventasFiltradas;
        }

    }
}