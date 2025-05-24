using CareNet_System.Models;
using CareNet_System.Repository;
using Microsoft.EntityFrameworkCore;
public class StaffRepository : IRepository<Staff>
{
    private readonly HosPitalContext _context;

    public StaffRepository(HosPitalContext context)
    {
        _context = context;
    }

    public List<Staff> GetAll()
    {
        return _context.Staff.Include(s => s.department).ToList();
    }

    public Staff GetById(int id)
    {
        return _context.Staff
            .Include(s => s.department)
            .FirstOrDefault(s => s.Id == id);
    }

    public void Add(Staff obj)
    {
        _context.Staff.Add(obj);
    }

    public void Update(Staff obj)
    {
        _context.Staff.Update(obj);
    }

    public void Delete(int id)
    {
        var staff = _context.Staff.Find(id);
        if (staff != null)
        {
            _context.Staff.Remove(staff);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
