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
        Task <IEnumerable<Venta>> GetSalesAfter (DateTime BaseDate);
        Task <IEnumerable<Venta>> GetSalesPerEmployee (int EmployeeId);
    }
}