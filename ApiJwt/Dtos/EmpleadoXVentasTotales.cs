using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJwt.Dtos
{
    public class EmpleadoXVentasTotales
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public DateTime FechaContratacion { get; set; }
        public int VentasTotales { get; set; }
    }
}