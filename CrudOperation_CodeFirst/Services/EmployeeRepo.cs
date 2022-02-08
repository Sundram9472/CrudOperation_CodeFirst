using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Models;
using CrudOperation_CodeFirst.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation_CodeFirst.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _Context;
        public EmployeeRepo(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            var DataList = _Context.Employee_sk.ToListAsync();
            return await DataList;
        }

        public async Task<Department> GetmployeetById(int? id)
        {
            var query = _Context.Department_sk
                                .Include(x => x.employees)
                                .Where(x => x.employees.EmployeeId == id)
                                .FirstOrDefaultAsync();
            return await query;
        }



        public async Task<Boolean> AddNewEmployee(Employee Data)
        {
            try
            {
                _Context.Employee_sk.Add(Data);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<Boolean> DeleteEmployee(int? Id)
        {
            var employeeData = _Context.Employee_sk.Where(x => x.EmployeeId == Id).FirstOrDefault();
            if (employeeData != null)
            {
                _Context.Employee_sk.Remove(employeeData);
                await _Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> EditEmployeeDataDep(Department data)
        {


            var editData = _Context.Employee_sk.Where(x => x.EmployeeId == data.employees.EmployeeId).FirstOrDefault();
            if (editData != null)
            {
                editData.EmployeeName = data.employees.EmployeeName;
                editData.EmployeeGmail = data.employees.EmployeeGmail;
                editData.EmployeeContact = data.employees.EmployeeContact;
                editData.EmployeeAdress = data.employees.EmployeeAdress;
                await _Context.SaveChangesAsync();
                return true; ;

            }
            else
            {
                return false;
            }
        }
      
        public async Task<Boolean> EditEmployeeData(Employee data)
        {


            var editData = _Context.Employee_sk.Where(x => x.EmployeeId == data.EmployeeId).FirstOrDefault();
            if (editData != null)
            {
                editData.EmployeeName = data.EmployeeName;
                editData.EmployeeGmail = data.EmployeeGmail;
                editData.EmployeeContact = data.EmployeeContact;
                editData.EmployeeAdress = data.EmployeeAdress;
                await _Context.SaveChangesAsync();
                return true; ;

            }
            else
            {
                return false;
            }
        }
    }
}
