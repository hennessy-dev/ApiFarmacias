using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class CompraConfiguration : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("compra");
            builder.Property(p=>p.FechaCompra).HasColumnType("datetime").IsRequired().HasColumnName("FechaCompra");
            builder.HasOne(p => p.Proveedor).WithMany(p=>p.Compras).HasForeignKey(p=>p.ProveedorId);
            
        }
    }
}