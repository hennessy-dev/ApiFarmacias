using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Interfaces
{
    public interface IPaciente : IGenericRepository<Paciente>
    {
        Task<IEnumerable<Paciente>> PatientsWhoHavePurchased (string drugName);
        Task<(Paciente p, double total)> PatientWhoSpentMostMoney ();
        Task<IEnumerable<Paciente>> PatientsWhoBoughtInLastYear (string drugName);
        Task<IEnumerable<Paciente>> PatientsWhoHaventBoughtAnythingBetween(DateTime initialDate, DateTime lastDate);
    }
}