using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Services;
using CrudOperation_CodeFirst.Data;
using CrudOperation_CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation_CodeFirst.Controllers
{
    [Route("[Controller]/[action]")]
    public class DepartmentsAjax : Controller
    {
        private readonly IDepartmentRepo _DepartmentDetails;
        private readonly AppDbContext _context;

        public DepartmentsAjax (IDepartmentRepo departmentdetails,AppDbContext appdbcontext)
        {
            _DepartmentDetails = departmentdetails;
            _context = appdbcontext;
        }

        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllDepList()
        {
           
            try
            {
                var data = await _DepartmentDetails.GetAllDepartment();
                if (data != null)
                {
                    return new JsonResult(data);
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

        [HttpPost]
        public async Task<IActionResult> Create(Department Data)
        {
           try
           {
                if (ModelState.IsValid)
                {
                   var data = await _DepartmentDetails.AddNewDepartment(Data);
                    return new JsonResult(data);
                }
                else
                {
                    return BadRequest("Enter required fields");
                }
           }
            catch(Exception ex)
           {
                throw new Exception(ex.Message);
           }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            try
            {
                if (Id != null)
                {
                    var departmentDetailsById = await _DepartmentDetails.GetDepartmentById(Id);
                    if(departmentDetailsById != null)
                    {
                        return new JsonResult(departmentDetailsById);
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            try
            {
                var checkEmployee = await _context.Employee_sk.CountAsync(x => x.DepartmentId == Id);
                if (checkEmployee > 0)
                {
                    return BadRequest("You are Not Delete Department Beacause This Department Related Employee Found ! Firstly Remove Employee Then Remove Department");
                }
                else
                {
                    var deleteData = await _DepartmentDetails.DeleteDepartment(Id);
                    if (deleteData != null)
                    {
                        return new JsonResult(true);
                    }
                    else
                    {
                        throw new Exception("Department Not Found");
                    }
                 
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var editConfiremation = await _DepartmentDetails.EditDepartmentData(department);
                    if (editConfiremation != null)
                    {
                        return new JsonResult(true);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
               
            }
            else
            {
                throw new Exception();
            }
            
        }

    }
}
