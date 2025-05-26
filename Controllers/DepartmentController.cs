using CareNet_System.Repostatory;
using Microsoft.AspNetCore.Mvc;
using CareNet_System.Models;

namespace CareNet_System.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository DeptRepo; 

        public DepartmentController(IDepartmentRepository _deptRepo)
        {
            DeptRepo = _deptRepo;
        }

        public IActionResult AllDepts() {
            List<Department> DeptList = DeptRepo.GetAll();
            return View("All",DeptList);
        }
        public IActionResult NewDept(Department newDept) { 
            Department dept = new Department();

            

            if (ModelState.IsValid)
            {
                dept.name = newDept.name;
                dept.manager = newDept.manager;
                dept.employees_num = newDept.employees_num;
                dept.Patients_num = newDept.Patients_num;

                DeptRepo.Add(dept);
                DeptRepo.Save();
                return RedirectToAction("AllDepts");

            }
            return View("New", newDept);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department dept = DeptRepo.GetById(id); 
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept); 
        }


        [HttpPost]
        public IActionResult Edit(  Department deptFromDB) {

            if (ModelState.IsValid)
            {
               

                DeptRepo.Update(deptFromDB);
                DeptRepo.Save();
                List<Department> alldepts = DeptRepo.GetAll(); 

                return View("All",alldepts);
            }
            return View(deptFromDB);
        
        }
       public IActionResult Delete(int id)
        {
            DeptRepo.Delete(id);
            DeptRepo.Save();

            List<Department> alldepts = DeptRepo.GetAll();

            return View("All", alldepts);
        }



    }
}
