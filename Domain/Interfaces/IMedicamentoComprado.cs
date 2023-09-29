using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicamentoComprado : IGenericRepository<MedicamentoComprado>
    {
        Task<IEnumerable<MedicamentoComprado>> GetDrugPurchasedFrom (int ProveedorId);
    }
}