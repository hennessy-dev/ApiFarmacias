using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MedicamentoVendidoConfiguration : IEntityTypeConfiguration<MedicamentoVendido>
    {
        public void Configure(EntityTypeBuilder<MedicamentoVendido> builder)
        {
            builder.ToTable("MedicamentoVendido");
            builder.Property(p=>p.CantidadVendida).HasColumnType("int").HasColumnName("CantidadVendida").IsRequired();
            builder.Property(p=>p.Precio).HasColumnType("decimal(10,10)").HasColumnName("Precio").IsRequired();
            builder.HasOne(p => p.Venta).WithMany(p=>p.MedicamentosVendidos).HasForeignKey(p=>p.VentaId);
            builder.HasOne(p => p.Medicamento).WithMany(p=>p.MedicamentosVendidos).HasForeignKey(p=>p.MedicamentoId);
        }
    }
}