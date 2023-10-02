using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
    {
        private FarmaciaContext _context { get; set; }

        public ProveedorRepository(FarmaciaContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> ProvidersWhoHaveNotSoldInLastYear()
        {
            var UltimoAño = DateTime.Now.AddYears(-1);

            var proveedoresFiltrados = await _context.Proveedores
                .Where(
                    proveedor =>
                        !_context.Compras.Any(
                            compra =>
                                compra.ProveedorId == proveedor.Id
                                && compra.FechaCompra >= UltimoAño
                        )
                )
                .ToListAsync();
            return proveedoresFiltrados;
        }

        public async Task<(
            Proveedor p,
            int totalMedicamentos
        )> SupplierThatHasSuppliedMoreInLastYear()
        {
            var currentDate = DateTime.Now;
            var oneYearAgo = currentDate.AddYears(-1);

            var supplierWithMostSupply = await _context.MedicamentosComprados
                .Where(
                    mc =>
                        mc.Compra.FechaCompra >= oneYearAgo && mc.Compra.FechaCompra <= currentDate
                )
                .GroupBy(mc => mc.Compra.Proveedor)
                .OrderByDescending(g => g.Sum(mc => mc.CantidadComprada))
                .Select(
                    g =>
                        new
                        {
                            Proveedor = g.Key,
                            TotalMedicamentos = g.Sum(mc => mc.CantidadComprada)
                        }
                )
                .FirstOrDefaultAsync();

            if (supplierWithMostSupply == null)
            {
                return (null, 0);
            }

            return (supplierWithMostSupply.Proveedor, supplierWithMostSupply.TotalMedicamentos);
        }

        public async Task<IEnumerable<Proveedor>> SuppliersThatHasSuppliedInLastYear()
        {
            var currentDate = DateTime.Now;
            var oneYearAgo = currentDate.AddYears(-1);

            var suppliers = await _context.Proveedores
                .Include(p=>p.Compras)
                .Where(
                    p =>
                        p.Compras.Any(c=>c.FechaCompra >= oneYearAgo && c.FechaCompra <= currentDate)
                ).ToListAsync();

            return suppliers;
        }

        public async Task<IEnumerable<Proveedor>> SuppliersOfMedicinesWithLessThan50Units()
        {
            var Proveedores = await _context.Proveedores.Include(p=>p.Medicamentos.Where(m=> m.Stock < 50))
            .Where(p=> p.Medicamentos.Count()>0)
            .ToListAsync();
            return Proveedores;
        }

        public async Task<IEnumerable<Proveedor>> SuppliersWith5OrMoreKindsOfMedicine(DateTime firtsDate, DateTime lastDate)
        {
            var MedicamentosComprados = await _context.MedicamentosComprados.Include( p=> p.Compra).Where(m=>m.Compra.FechaCompra >= firtsDate && m.Compra.FechaCompra <= lastDate).ToListAsync();
            var compras = MedicamentosComprados.Select(m=>m.Compra).ToList();
            var groups = compras.GroupBy(m=>m.ProveedorId).Where(g=> g.Count() >= 5).Select(g=>g.Key);
            var proveedores = await _context.Proveedores.Include(p=>p.Medicamentos).Where(p=> groups.Contains(p.Id)).ToListAsync();
            return proveedores;

        }
    }
}
