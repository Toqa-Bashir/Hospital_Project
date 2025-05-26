using Microsoft.AspNetCore.Mvc;
using CareNet_System.Repostatory;
using CareNet_System.Models;
using CareNet_System.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareNet_System.Repository;


namespace CareNet_System.Controllers
{
    public class PatientController : Controller
    {
        IPatientRepository PatientReop;
        IDepartmentRepository DeptRepo;
        IStaffRepository StaffRepo;

        public PatientController(IPatientRepository patientrepo, IDepartmentRepository deptrepo, IStaffRepository sttaffrepo)
        {
            PatientReop = patientrepo;
            DeptRepo = deptrepo;
            StaffRepo = sttaffrepo;

        }
        public IActionResult ShowAllPatients()
        {
            List<Patient> patientsFromDB = PatientReop.GetAll();

            List<PatientDetailsVM> viewModelList = patientsFromDB.Select(p => new PatientDetailsVM
            {
                PId = p.Id,
                PName = p.name,
                PRoomNum = p.room_num,
                PDoctorName = p.staff != null ? p.staff.name : "",
                PDepartment = p.department != null ? p.department.name : ""
            }).ToList();
            return View("ShowAllPatients", viewModelList);
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidatePatientName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json("Name is required");

            var words = name.Trim().Split(' ');
            if (words.Length < 3)
                return Json("Name must contain at least 3 words");

            return Json(true);
        }

        public IActionResult AddNewPatient()
        {
            List<Staff> doctors = StaffRepo.GetAll().Where(s => s.title == StaffTitle.Doctor).ToList();
            ViewBag.DoctorsList = new SelectList(doctors, "Id", "name");
            ViewBag.DeptList = DeptRepo.GetAll();
            return View("AddNewPatient");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SaveNew(AddPatientVM NewPatient)
        {
            if (ModelState.IsValid)
            {


                Patient newpatient = new Patient
                {
                    name = NewPatient.Name,
                    room_num = NewPatient.RoomNum,
                    dept_id = (int)NewPatient.DeptId,
                    followUp_doctorID = (int)NewPatient.followUp_doctorID,
                };


               
                PatientReop.Add(newpatient);
                PatientReop.Save();
               

                return RedirectToAction("ShowAllPatients");
            }
            else
            {
                ViewBag.DeptList = DeptRepo.GetAll();
                return View("AddNewPatient", NewPatient);
            }
        }
        public IActionResult UpdatePatient(int id)
        {
            Patient patientfromDB = PatientReop.GetBYid(id);

            if (patientfromDB == null)
            {
                return NotFound();
            }
           
            ViewBag.DeptList = new SelectList(DeptRepo.GetAll(), "Id", "name");

          
            ViewBag.DoctorsList = new SelectList(
                StaffRepo.GetAll().Where(s => s.title == StaffTitle.Doctor).ToList(),
                "Id", "name"
            );


            AddPatientVM patientVm = new AddPatientVM
            {
                Id = patientfromDB.Id,
                Name = patientfromDB.name,
                RoomNum = patientfromDB.room_num,
                DeptId = patientfromDB.dept_id,
                followUp_doctorID = patientfromDB.followUp_doctorID,
                Departments = DeptRepo.GetAll()
            };
            ViewBag.DeptList = DeptRepo.GetAll();
            ViewBag.DoctorsList = new SelectList(
                StaffRepo.GetAll().Where(s => s.title == StaffTitle.Doctor).ToList(),
                "Id", "name"
            );

            return View("UpdatePatient", patientVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEditPatient(AddPatientVM patientFromReq)
        {
            if (ModelState.IsValid)
            {
                Patient patient = PatientReop.GetBYid(patientFromReq.Id);

                if (patient == null)
                {
                    return NotFound();
                }

                patient.name = patientFromReq.Name;
                patient.room_num = patientFromReq.RoomNum;
                patient.dept_id = (int)patientFromReq.DeptId;
                patient.followUp_doctorID = (int)patientFromReq.followUp_doctorID;


                PatientReop.Update(patient);
                PatientReop.Save();

                return RedirectToAction("ShowAllPatients");
            }

           
            ViewBag.DeptList = DeptRepo.GetAll();
            ViewBag.DoctorsList = new SelectList(
                StaffRepo.GetAll().Where(s => s.title == StaffTitle.Doctor).ToList(),
                "Id", "name"
            );
            return View("UpdatePatient", patientFromReq);
        }
        public IActionResult DeletePatient(int id)
        {
            var patient = PatientReop.GetBYid(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View("ConfirmDelete", patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            var patient = PatientReop.GetBYid(id);
            if (patient == null)
            {
                return NotFound();
            }

            PatientReop.Delete(id);
            PatientReop.Save();
            return RedirectToAction("ShowAllPatients");
        }




    }
}