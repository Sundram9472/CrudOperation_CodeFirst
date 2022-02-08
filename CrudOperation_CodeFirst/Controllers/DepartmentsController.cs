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
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepo _DepartmentDetails;
        private readonly AppDbContext _Context;
        public DepartmentsController(IDepartmentRepo departmentdetails,AppDbContext appdbcontext)
        {
            _DepartmentDetails = departmentdetails;
            _Context = appdbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var allDepartment = await _DepartmentDetails.GetAllDepartment();
                if (allDepartment != null)
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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error","Error");
            }
          
            try
            {
                var departmentDetailsById = await _DepartmentDetails.GetDepartmentById(id);
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

        [HttpGet]
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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var departmentDetails = await _DepartmentDetails.GetDepartmentById(id);
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

        [Route("{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Department department)
        {
                try
                {
                    if (ModelState.IsValid)
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
                    else
                    {
                        throw new Exception("Model Is Not Valid");
                    }
                }
                catch (Exception ex)
                {
                     throw new Exception(ex.Message);
                }
        }

        [Route("{id}")]
        [HttpGet]
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

        [Route("{id}")]
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
