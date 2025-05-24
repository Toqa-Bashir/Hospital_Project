using CareNet_System.Models;

namespace CareNet_System.Repository
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<List<Staff>> GetStaffByTitleAsync(StaffTitle title);
        Task<List<Staff>> GetSeniorStaffAsync();
    }
}
