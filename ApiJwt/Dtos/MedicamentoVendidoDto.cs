using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJwt.Dtos
{
    public class MedicamentoVendidoDto
    {
        public int Id { get; set;}
        public int VentaId { get; set; }
        public int MedicamentoId { get; set; }
        public int CantidadVendida { get; set; }
        public decimal Precio { get; set; }
    }
}