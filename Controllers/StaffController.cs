using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareNet_System.Models;
using CareNet_System.Repository;
using Microsoft.AspNetCore.Authorization;
using CareNet_System.Repostatory;

namespace CareNet_System.Controllers
{

    public class StaffController : Controller
    {

        IStaffRepository StfRepo;

        public StaffController(IStaffRepository _stfRepo)
        {
            StfRepo = _stfRepo;
        }


        public IActionResult AllStaff()
        {
            List<Staff> StfList = StfRepo.GetAll();
            return View("All", StfList);
        }
        public IActionResult NewStf(Staff newStf)
        {
            Staff stf = new Staff();

            if (ModelState.IsValid)
            {
                stf.name = newStf.name;
                stf.title = newStf.title;
                stf.salary = newStf.salary;
                stf.seniority_level = newStf.seniority_level;
                stf.experience_years = newStf.experience_years;
                stf.dept_id= newStf.dept_id;
                stf.personal_photo= newStf.personal_photo;

                StfRepo.Add(stf);
                StfRepo.Save();
                return RedirectToAction("AllStaff");

            }
            return View("New", newStf);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Staff stf = StfRepo.GetById(id);
            if (stf == null)
            {
                return NotFound();
            }

            return View(stf);
        }


        [HttpPost]
        public IActionResult Edit(Staff StfFromDB)
        {

            if (ModelState.IsValid)
            {


                StfRepo.Update(StfFromDB);
                StfRepo.Save();
                List<Staff> allstaff = StfRepo.GetAll();

                return View("All", allstaff);
            }
            return View(StfFromDB);

        }

        public IActionResult Delete(int id)
        {
            StfRepo.Delete(id);
            StfRepo.Save();

            List<Staff> allstaff = StfRepo.GetAll();

            return View("All", allstaff);
        }

    }
}

