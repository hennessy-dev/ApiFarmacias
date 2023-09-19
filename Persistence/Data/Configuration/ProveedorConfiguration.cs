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
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedor");
            builder.Property(p=>p.Nombre).IsRequired().HasMaxLength(120).HasColumnName("Nombre");
            builder.Property(p=>p.Email).IsRequired().HasMaxLength(80).HasColumnName("Email");
            builder.Property(p=>p.Telefono).IsRequired().HasMaxLength(28).HasColumnName("Telefono");
            builder.Property(p=>p.Direccion).IsRequired().HasMaxLength(200).HasColumnName("Direccion");

        }
    }
}