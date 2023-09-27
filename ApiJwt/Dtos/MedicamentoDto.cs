using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiJwt.Dtos
{
    public class MedicamentoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int ProovedorId { get; set; }
    }
}