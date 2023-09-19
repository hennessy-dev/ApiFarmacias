using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MedicamentoCompradoConfiguration : IEntityTypeConfiguration<MedicamentoComprado>
    {
        public void Configure(EntityTypeBuilder<MedicamentoComprado> builder)
        {
            builder.ToTable("MedicamentoComprado");
            builder.Property(p=>p.CantidadComprada).HasColumnName("CantidadComprada").HasColumnType("int").IsRequired();
            builder.Property(p=>p.PrecioCompra).HasColumnName("PrecioCompra").HasColumnType("decimal(10,10)").IsRequired();
            builder.HasOne(p => p.Compra).WithMany(p=>p.MedicamentosComprados).HasForeignKey(p=>p.CompraId);
            builder.HasOne(p => p.Medicamento).WithMany(p=>p.MedicamentosComprados).HasForeignKey(p=>p.MedicamentoId);
            
        }
    }
}