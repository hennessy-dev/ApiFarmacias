using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Venta");
            builder.Property(p=>p.FechaVenta).HasColumnType("datetime").IsRequired().HasColumnName("FechaVenta");
            builder.HasOne(p => p.Paciente).WithMany(p => p.Ventas).HasForeignKey(p => p.PacienteId);
            builder.HasOne(p => p.Empleado).WithMany(p => p.Ventas).HasForeignKey(p => p.EmpleadoId);
        }
    }
}