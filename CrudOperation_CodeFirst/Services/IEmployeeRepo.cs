using CrudOperation_CodeFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperation_CodeFirst.Services
{
    public interface IEmployeeRepo
    {
        Task<bool> AddNewEmployee(Employee Data);
        Task<bool> DeleteEmployee(int? Id);
        Task<bool> EditEmployeeData(Employee data);
        Task<bool> EditEmployeeDataDep(Department data);
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Department> GetmployeetById(int? id);
    }
}