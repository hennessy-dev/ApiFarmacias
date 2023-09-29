using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVenta : IGenericRepository<Venta>
    {
        //2023-02-01T05:00:00.000Z
        Task <IEnumerable<Venta>> GetSalesAfter (DateTime BaseDate);
    }
}