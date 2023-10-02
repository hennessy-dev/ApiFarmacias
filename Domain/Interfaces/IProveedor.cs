using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProveedor : IGenericRepository<Proveedor>
    {
        Task<IEnumerable<Proveedor>> ProvidersWhoHaveNotSoldInLastYear();
        Task <(Proveedor p, int totalMedicamentos)> SupplierThatHasSuppliedMoreInLastYear ();
        Task <IEnumerable<Proveedor>> SuppliersThatHasSuppliedInLastYear ();
        Task<IEnumerable<Proveedor>> SuppliersOfMedicinesWithLessThan50Units();
        Task<IEnumerable<Proveedor>> SuppliersWith5OrMoreKindsOfMedicine (DateTime firtsDate,DateTime lastDate); 
    }
}