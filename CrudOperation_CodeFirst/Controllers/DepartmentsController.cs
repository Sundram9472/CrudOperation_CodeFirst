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
        private readonly AppDbContext _Context;
        public DepartmentsController(IDepartmentRepo departmentdetails,AppDbContext appdbcontext)
        {
            _DepartmentDetails = departmentdetails;
            _Context = appdbcontext;
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
                return View("Error","Error");
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
          
            try
            {
                var CountEployee = await _Context.Employee_sk.CountAsync(x => x.DepartmentId == id);
                if (CountEployee > 0)
                {
                        var allDepartment = await _DepartmentDetails.GetAllDepartment();
                        if (allDepartment != null)
                        {
                            ViewData["Message"] = "You are Not Delete Department Beacause This Department Related Employee Found ! Firstly Remove Employee Then Remove Department";
                            return View("Index",allDepartment);
                        }
                        else
                        {
                            return NotFound();
                        }
                }
                else
                {
                    var deleteDataConfirmed = await _DepartmentDetails.DeleteDepartment(id);
                    if (deleteDataConfirmed != null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
