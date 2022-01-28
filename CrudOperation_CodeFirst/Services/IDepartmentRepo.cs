using CrudOperation_CodeFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudOperation_CodeFirst.Services
{
    public interface IDepartmentRepo
    {
        Task<Department> AddNewDepartment(Department Data);
        Task<Department> DeleteDepartment(int? Id);
        Task<Department> EditDepartmentData(Department data);
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<Department> GetDepartmentById(int? id);
    }
}