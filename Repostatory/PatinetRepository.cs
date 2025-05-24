using CareNet_System.Models;
using CareNet_System.Repository;
using Microsoft.EntityFrameworkCore;


namespace CareNet_System.Repostatory
{
    public class PatientRepository : IPatientRepository
    {
        HosPitalContext context;
        public PatientRepository(HosPitalContext cnt)
        {
            context = cnt;
        }
        public void Add(Patient obj)
        {
            context.Patients.Add(obj);
        }


        public List<Patient> GetAll()
        {
            return context.Patients.Include(p => p.staff).Include(p => p.department).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Patient obj)
        {
            Patient orgpatient = GetBYid(obj.Id);
            orgpatient.name = obj.name;
            orgpatient.room_num = obj.room_num;
            orgpatient.followUp_doctorID = obj.followUp_doctorID;
            orgpatient.dept_id = obj.dept_id;

        }

        public void Delete(int id)
        {
            Patient p = GetBYid(id);
            if (p != null)
            {
                context.Patients.Remove(p);
            }
            else
            {
                throw new Exception("Patient not found");
            }
        }
        public Patient GetBYid(int id)
        {
            return context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient GetById(int id)
        {
            throw new NotImplementedException();
        }
    }


}