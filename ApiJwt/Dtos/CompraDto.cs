using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiJwt.Dtos
{
    public class CompraDto
    {
        public int Id { get; set; }
        public DateTime FechaCompra { get; set; }
        public int ProveedorId { get; set; }
    }
}