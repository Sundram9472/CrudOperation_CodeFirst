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
    [Route("[Controller]/[action]")]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmployeeRepo _employeeDetails;
        public EmployeesController(AppDbContext context,IEmployeeRepo contetxtE)
        {
            _context = context;
            _employeeDetails = contetxtE;
        }

        [HttpGet]
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

        [Route("{id}")]
        [HttpGet]
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

        [HttpGet]
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

        [Route("{id}")]
        [HttpGet]
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

        [Route("{id}")]
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Department data)
        {
                try
                {
                    
                        Boolean editConfiremation = await _employeeDetails.EditEmployeeDataDep(data);
                        if (editConfiremation)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return NotFound();
                        }
                }
                catch
                {
                   throw new Exception("Model Is Not Validate Some Issue Occcured");
                }
               
        }

        [Route("{id}")]
        [HttpGet]
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

        [Route("{id}")]
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
