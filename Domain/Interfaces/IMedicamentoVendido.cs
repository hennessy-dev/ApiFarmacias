using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicamentoVendido : IGenericRepository<MedicamentoVendido>
    {
        Task<IEnumerable<MedicamentoVendido>> GetDrugsSoldAfter(IEnumerable<Venta> ventas);
        Task<int> GetTotalDrugsSold (string drugName);
        Task<double> GetTotalPriceDrugSold ();
        Task<IEnumerable<MedicamentoVendido>> GetDrugSoldAfterAndBeforeThan (DateTime firtsDate, DateTime lastDate);
        Task<double> AverageDrugSoldPerSale ();
    }
}