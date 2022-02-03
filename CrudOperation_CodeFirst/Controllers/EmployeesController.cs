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
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepo _employeeDetails;
        public EmployeesController(AppDbContext context,IEmployeeRepo contetxtE)
        {
            _context = context;
            _employeeDetails = contetxtE;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            try
            {
                var getEmployeeList = await _employeeDetails.GetAllEmployee();
                if (getEmployeeList != null)
                {
                    return View(getEmployeeList);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var employeeDetails = await _employeeDetails.GetmployeetById(id);
                    if (employeeDetails != null)
                    {
                        return View(employeeDetails);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                throw;
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            try
            {
                var depList = _context.Department_sk.ToList();
                var depcheck = _context.Department_sk.Count();
                if (depcheck > 0)
                {
                    ViewBag.Department_Id = new SelectList(depList, nameof(Department.DepartmentId), nameof(Department.DepartmentName));
                    return View();
                }
                else
                {
                    throw new Exception("No Department Found");
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
          Boolean createDataDetails = false;
            try
            {
                if (ModelState.IsValid)
                {
                    createDataDetails = await _employeeDetails.AddNewEmployee(employee);
                }
                if (createDataDetails )
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeeDetails = await _employeeDetails.GetmployeetById(id);
            try
            {
                if (employeeDetails != null)
                {
                    return View(employeeDetails);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
                try
                {
                    if (ModelState.IsValid)
                    {
                        Boolean editConfiremation = await _employeeDetails.EditEmployeeData(employee);
                        if (editConfiremation)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return NotFound();
                        }
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
               
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var employeeDetails = await _employeeDetails.GetmployeetById(id);
                    if (employeeDetails != null)
                    {
                        return View(employeeDetails);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                throw;
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Boolean deleteDataConfirmed = await _employeeDetails.DeleteEmployee(id);
            try
            {
                if (deleteDataConfirmed)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
