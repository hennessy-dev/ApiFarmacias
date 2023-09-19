using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleado");
            builder.Property(e => e.Nombre).HasColumnName("Nombre").HasMaxLength(150).IsRequired();
            builder.Property(e => e.Cargo).HasColumnName("Cargo").HasMaxLength(150).IsRequired();
            builder.Property(p=>p.FechaContratacion).HasColumnType("datetime").IsRequired().HasColumnName("FechaContratacion");        
        }
    }
}