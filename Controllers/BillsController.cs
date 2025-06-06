﻿using CareNet_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CareNet_System.Models;
using CareNet_System.Repository;
using CareNet_System.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CareNet_System.Repostatory;

namespace CareNet_System.Controllers
    {
        public class BillsController : Controller
        {
            private readonly IBillsRepository _billRepository;
            private readonly HosPitalContext _context;

            public BillsController(IBillsRepository billRepository, HosPitalContext context)
            {
                _billRepository = billRepository;
                _context = context;
            }

            // GET: Bills
            public IActionResult Index()
            {
                var bills = _billRepository.GetAll();
                return View(bills);
            }

            // GET: Bills/Details/5
            public IActionResult Details(int id)
            {
                var bill = _billRepository.GetById(id);
                if (bill == null)
                {
                    return NotFound();
                }
                return View(bill);
            }

            // GET: Bills/Create
            public IActionResult Create()
            {
                var viewmodel = new BillsViewModels();
                var patients = _context.Patients
                    .OrderBy(p => p.name)
                    .Select(p => new { p.Id, p.name })
                    .ToList();

                ViewBag.Patients = patients;
                return View(viewmodel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(BillsViewModels billViewModel)
            {
                try
                {
                    var bill = new Bills
                    {
                        total_amount = billViewModel.total_amount,
                        patient_id = billViewModel.patient_id,
                        insurance_id = billViewModel.insurance_id
                    };

                    _billRepository.Add(bill);
                    _billRepository.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
                var patients = _context.Patients
                    .OrderBy(p => p.name)
                    .Select(p => new { p.Id, p.name })
                    .ToList();

                ViewBag.Patients = patients;
                return View(billViewModel);
            }

            // GET: Bills/Edit/5
            public IActionResult Edit(int id)
            {
                var patients = _context.Patients
                    .OrderBy(p => p.name)
                    .Select(p => new { p.Id, p.name })
                    .ToList();

                ViewBag.Patients = patients;

                var bill = _billRepository.GetById(id);
                if (bill == null)
                {
                    return NotFound();
                }

                var billViewModel = new BillsViewModels
                {
                    Id = bill.Id,
                    total_amount = bill.total_amount,
                    Payment_Method = bill.Payment_Method.ToString(),
                    patient_id = bill.patient_id,
                    insurance_id = bill.insurance_id
                };

                return View("Edit", billViewModel);
            }

            // POST: Bills/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, BillsViewModels billViewModel)
            {
                if (id != billViewModel.Id)
                {
                    return NotFound();
                }

                try
                {
                    var bill = _billRepository.GetById(id);
                    if (bill == null)
                    {
                        return NotFound();
                    }

                    bill.total_amount = billViewModel.total_amount;
                    bill.patient_id = billViewModel.patient_id;
                    bill.insurance_id = billViewModel.insurance_id;

                    _billRepository.Update(bill);
                    _billRepository.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.Message);
                }

                var patients = _context.Patients
                    .OrderBy(p => p.name)
                    .Select(p => new { p.Id, p.name })
                    .ToList();

                ViewBag.Patients = patients;
                return View(billViewModel);
            }

            // GET: Bills/Delete/5
            public IActionResult Delete(int id)
            {
                var bill = _billRepository.GetById(id);
                if (bill == null)
                {
                    return NotFound();
                }

                var billVM = new BillsViewModels()
                {
                    Id = bill.Id,
                    total_amount = bill.total_amount
                };

                return View("Delete", billVM);
            }

            // POST: Bills/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                _billRepository.Delete(id);
                _billRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            // GET: Bills/PatientBills/5
            //public IActionResult PatientBills(int id)
            //{
            //    var bills = _billRepository.GetBillsByPatientId(id);
            //    if (bills == null || !bills.Any())
            //    {
            //        return NotFound();
            //    }

            //    ViewBag.PatientId = id;
            //    ViewBag.PatientName = bills.FirstOrDefault()?.patient?.name ?? "Patient";
            //    return View("PatientBills");
            //}

            //// GET: Bills/InsuranceBills/5
            //public IActionResult InsuranceBills(int id)
            //{
            //    var bills = _billRepository.GetBillsByInsuranceId(id);
            //    if (bills == null || !bills.Any())
            //    {
            //        return NotFound();
            //    }

            //    ViewBag.InsuranceId = id;
            //    return View(bills);
            //}

            private bool BillExists(int id)
            {
                return _billRepository.GetById(id) != null;
            }

        }
    }


