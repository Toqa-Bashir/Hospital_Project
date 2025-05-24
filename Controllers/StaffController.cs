using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareNet_System.Models;
using CareNet_System.Repository;
using Microsoft.AspNetCore.Authorization;

namespace CareNet_System.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IRepository<Staff> _staffRepository;

        public StaffController(IRepository<Staff> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        // GET: Staffs
        public IActionResult Index(string titleFilter, int? departmentFilter)
        {
            var staffList = _staffRepository.GetAll();

            // Set selected value for TitleList (so the filter persists)
            ViewBag.TitleList = new SelectList(
                Enum.GetValues(typeof(StaffTitle))
                    .Cast<StaffTitle>()
                    .Select(t => new { Value = t.ToString(), Text = t.ToString() }),
                "Value",
                "Text",
                titleFilter
            );

            // Set selected value for DepartmentList
            var departments = _staffRepository.GetAll()
                .Select(s => s.department)
                .Where(d => d != null)
                .Distinct()
                .ToList();

            ViewBag.DepartmentList = new SelectList(
                departments,
                "Id",
                "name",
                departmentFilter
            );

            // Apply filtering if selected
            if (!string.IsNullOrEmpty(titleFilter))
            {
                staffList = staffList.Where(s => s.title.ToString() == titleFilter).ToList();
            }

            if (departmentFilter.HasValue)
            {
                staffList = staffList.Where(s => s.dept_id == departmentFilter.Value).ToList();
            }

            return View(staffList);
        }

        // GET: Staffs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var staff = _staffRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (staff == null) return NotFound();
            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewBag.dept_id = new SelectList(_staffRepository.GetAll()
                .Select(s => s.dept_id)
                .Distinct()
                .Select(id => new { Value = id, Text = id.ToString() }),
                "Value", "Text");

            ViewBag.TitleList = Enum.GetValues(typeof(StaffTitle))
                .Cast<StaffTitle>()
                .Select(t => new SelectListItem { Value = t.ToString(), Text = t.ToString() })
                .ToList();

            ViewBag.LevelList = Enum.GetValues(typeof(Level))
                .Cast<Level>()
                .Select(l => new SelectListItem { Value = l.ToString(), Text = l.ToString() })
                .ToList();

            return View();
        }

        // POST: Staffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind("Id,name,title,salary,seniority_level,experience_years,personal_photo,dept_id")] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);  // 🔹 طباعة جميع الأخطاء
                }

                return View(staff);  // 🔹 إعادة عرض الصفحة مع عرض الأخطاء
            }

            _staffRepository.Add(staff);
            _staffRepository.Save();
            return RedirectToAction("Index");  // 🔹 العودة إلى قائمة الموظفين بعد الإنشاء
        }
        // GET: Staffs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var staff = _staffRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (staff == null) return NotFound();

            ViewBag.dept_id = new SelectList(_staffRepository.GetAll()
                .Select(s => new { Value = s.dept_id, Text = s.dept_id.ToString() })
                .Distinct(), "Value", "Text", staff.dept_id);

            return View(staff);
        }
        // POST: Staffs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,name,title,salary,seniority_level,experience_years,personal_photo,dept_id")] Staff staff)
        {
            if (id != staff.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _staffRepository.Update(staff);
                _staffRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            // 🔹 إعادة تمرير القوائم إذا فشل التحقق
            ViewData["dept_id"] = new SelectList(_staffRepository.GetAll(), "Id", "Id", staff.dept_id);

            ViewBag.TitleList = Enum.GetValues(typeof(StaffTitle))
                .Cast<StaffTitle>()
                .Select(t => new SelectListItem { Value = t.ToString(), Text = t.ToString() })
                .ToList();

            ViewBag.LevelList = Enum.GetValues(typeof(Level))
                .Cast<Level>()
                .Select(l => new SelectListItem { Value = l.ToString(), Text = l.ToString() })
                .ToList();

            return View(staff);
        }
        // GET: Staffs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var staff = _staffRepository.GetAll().FirstOrDefault(s => s.Id == id);
            if (staff == null) return NotFound();
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _staffRepository.Delete(id);
            _staffRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
