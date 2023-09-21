using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJwt.Dtos
{
    public class MedicamentoCompradoDto
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int MedicamentoId { get; set; }
        public int CantidadComprada { get; set; }
        public decimal PrecioCompra { get; set; }
    }
}