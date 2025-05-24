using CareNet_System.Models;
using CareNet_System.Repository;

namespace CareNet_System.Repostatory
{
    public interface IPatientRepository : IRepository<Patient>
    {
        public Patient GetBYid(int id);
        //Patient GetBYid(object id);
        //Patient GetBYid(object id);
    }
}