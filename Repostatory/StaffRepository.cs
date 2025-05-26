using CareNet_System.Models;
using CareNet_System.Repository;
using Microsoft.EntityFrameworkCore;
public class StaffRepository : IStaffRepository
{
    HosPitalContext context;

    public StaffRepository(HosPitalContext ctx)
    {
        context = ctx;

    }

    public List<Staff> GetAll()
    {

        return context.Staff.ToList();
    }

    public void Add(Staff obj)
    {
        context.Staff.Add(obj);
    }

    public void Update(Staff obj)
    {

        List<Staff> staffList = context.Staff.ToList();

        Staff stf = context.Staff.FirstOrDefault(s => s.Id == obj.Id);

      stf.name = obj.name;
        stf.title = obj.title;
        stf.salary = obj.salary;
        stf.seniority_level = obj.seniority_level;
        stf.experience_years = obj.experience_years;
    }
    public void Delete(int id)
    {
        context.Staff.Remove(context.Staff.FirstOrDefault(s => s.Id == id));
    }

    public void Save()
    {
        context.SaveChanges();
    }

    public Staff GetById(int id)
    {
        return context.Staff.FirstOrDefault(s => s.Id == id);



    }
}

