using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IMedicamento : IGenericRepository<Medicamento>
    {
        Task<IEnumerable<Medicamento>> GetLessThan50 ();
        Task<IEnumerable<Medicamento>> GetMedicamentoProveedor();
        Task UpdateStock (int medicamentoId,int cantidadVendida);
        Task <IEnumerable<Medicamento>> GetDrugExpiresBefore (DateTime dateBase);
        Task<IEnumerable<Medicamento>> GetUnsoldDrug();
        Task<Medicamento> GetMostExpensiveDrug ();
        Task<Medicamento> GetLeastSoldDrug ();
        Task<(List<Medicamento> medicamentos, List<int> totales)> GetTotalDrugSoldPer(DateTime initialDate, DateTime lastDate);
        Task<List<Medicamento>> GetTotalDrugUnsoldBetween(DateTime initialDate, DateTime lastDate);
        Task<List<Medicamento>> GetDrugWithPiceMoreThanAndStokLeastThan (double price, int stok);
    }
}