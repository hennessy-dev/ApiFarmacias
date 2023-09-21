using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJwt.Dtos
{
    public class VentaDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}