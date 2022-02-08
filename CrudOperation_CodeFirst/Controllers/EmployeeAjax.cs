using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Models;
using CrudOperation_CodeFirst.Services;
using CrudOperation_CodeFirst.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation_CodeFirst.Controllers
{
    [Route("[Controller]/[action]")]
    public class EmployeeAjax : Controller
    {
        private readonly IEmployeeRepo _EmployeeDetails;
        private readonly AppDbContext _context;
        private readonly IDepartmentRepo _DepartmentRepo;

        public EmployeeAjax(IEmployeeRepo employeedetails, AppDbContext context,IDepartmentRepo departmentRepo)
        {
            _EmployeeDetails = employeedetails;
            _context = context;
            _DepartmentRepo = departmentRepo;
        }

        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllDepList()
        {
            try
            {
                var departmentData = await _DepartmentRepo.GetAllDepartment();
                if(departmentData != null)
                {
                    return new JsonResult(departmentData);
                }
                else
                {
                    return BadRequest("Some Issue");
                }
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> AllEmpList()
        {
            var data = await _context.Department_sk
                              .Include(x => x.employees).ToListAsync();
            try
            {
                if (data != null)
                {
                    return new JsonResult(data);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("DepartmentId,EmployeeAdress,EmployeeContact,EmployeeGmail,EmployeeName")] Employee Data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Boolean data = await _EmployeeDetails.AddNewEmployee(Data);
                    if(data)
                    {
                        return new JsonResult(true);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    return BadRequest("Enter required fields");
                }
            }
            catch (Exception ex)
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
                    var departmentDetailsById = await _EmployeeDetails.GetmployeetById(Id);
                 
                    if (departmentDetailsById != null)
                    {
                        return new JsonResult(departmentDetailsById);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    return BadRequest("Not Found !");
                }
            }
            catch (Exception ex)
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
                if (Id != null)
                {
                    Boolean deleteData = await _EmployeeDetails.DeleteEmployee(Id);
                    if (deleteData)
                    {
                        return new JsonResult(true);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    return BadRequest("Not Found !");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee Data)
        {
              try
                {
                    if(ModelState.IsValid)
                    {
                        Boolean editConfiremation = await _EmployeeDetails.EditEmployeeData(Data);
                        if (editConfiremation)
                        {
                            return new JsonResult(true);
                        }
                        else
                        {
                            return BadRequest("Not Found !");
                        }
                    }
                    else
                    {
                       throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
        }
    }
}
