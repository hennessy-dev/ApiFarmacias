using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJwt.Dtos
{
    public class BalanceDto
    {
        public double TotalBought { get; set; }
        public double TotalSold { get; set; }
        public double Balance { get; set; }
    }
}