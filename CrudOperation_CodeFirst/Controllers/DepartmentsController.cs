using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudOperation_CodeFirst.Data;
using CrudOperation_CodeFirst.Models;
using CrudOperation_CodeFirst.Services;

namespace CrudOperation_CodeFirst.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepo _DepartmentDetails;

        public DepartmentsController(IDepartmentRepo departmentdetails)
        {
            _DepartmentDetails = departmentdetails;
        }


        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var allDepartment = await _DepartmentDetails.GetAllDepartment();
            try
            {
                if(allDepartment != null)
                {
                    return View(allDepartment);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentDetailsById = await _DepartmentDetails.GetDepartmentById(id);
            try
            {
                if (departmentDetailsById != null)
                {
                    return View(departmentDetailsById);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            var createDataDetails = department;
            try
            {
                if (ModelState.IsValid)
                {
                   createDataDetails = await _DepartmentDetails.AddNewDepartment(department);
                }
                if (createDataDetails != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
              var departmentDetails = await _DepartmentDetails.GetDepartmentById(id);
            try
            {
                if (departmentDetails != null)
                {
                    return View(departmentDetails);
                }
                else
                {
                    return NotFound();
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editConfiremation = await _DepartmentDetails.EditDepartmentData(department);
                    if (editConfiremation != null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var deletedataDetailsDeapartment = await _DepartmentDetails.GetDepartmentById(id);
                if (deletedataDetailsDeapartment != null)
                {
                    return View(deletedataDetailsDeapartment);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteDataConfirmed = await _DepartmentDetails.DeleteDepartment(id);
            try
            {
                if (deleteDataConfirmed != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
