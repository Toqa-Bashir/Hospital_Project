using System.Collections.Generic;
using CareNet_System.Models;
using CareNet_System.ViewModels;
using CareNet_System.Repository;


namespace CareNet_System.Repostatory
{
    public interface IBillsRepository
    {
        IEnumerable<Bills> GetAll();
        Bills? GetById(int id);
        void Add(Bills bill);
        void Update(Bills bill);
        void Delete(int id);
        void Save();
        IEnumerable<Bills> GetBillsByPatientId(int patientId);
        IEnumerable<Bills> GetBillsByInsuranceId(int insuranceId);
    }
}
