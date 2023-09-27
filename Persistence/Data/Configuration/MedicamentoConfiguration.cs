using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("Medicamento");
            builder.Property(x => x.Nombre).HasColumnName("Nombre").HasMaxLength(120).IsRequired();
            builder.Property(x => x.Precio).HasColumnName("Precio").HasColumnType("double").IsRequired();
            builder.Property(x => x.Stock).HasColumnName("Stock").HasColumnType("int").IsRequired();
            builder.Property(x => x.FechaExpiracion).HasColumnName("FechaExpiracion").HasColumnType("datetime").IsRequired();
            builder.HasOne(p=>p.Proveedor).WithMany(p=>p.Medicamentos).HasForeignKey(p=>p.ProovedorId);
        }
    }
}