using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJwt.Dtos
{
    public class MedicamentoXProveedorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public ProveedorDto ProveedorDto { get; set; }
    }
}