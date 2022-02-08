using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Models;
using CrudOperation_CodeFirst.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudOperation_CodeFirst.Services
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext _Context;
        public DepartmentRepo(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            var DataList = _Context.Department_sk.ToListAsync();
            return await DataList;
        }

        public async Task<Department> GetDepartmentById(int? id)
        {
            var Data = _Context.Department_sk.Where(x => x.DepartmentId == id).FirstOrDefaultAsync();
            return await Data;
        }



        public async Task<Department> AddNewDepartment(Department Data)
        {
            try
            {
                _Context.Department_sk.Add(Data);
                await _Context.SaveChangesAsync();
                return Data;
            }
            catch
            {
                return null;
            }

        }

        public async Task<Department> DeleteDepartment(int? Id)
        {
            var departmentDetails = _Context.Department_sk.Where(x => x.DepartmentId == Id).FirstOrDefault();
            if (departmentDetails != null)
            {
                _Context.Department_sk.Remove(departmentDetails);
                await _Context.SaveChangesAsync();
                return departmentDetails;
            }
            else
            {
                return null;
            }
        }

        public async Task<Department> EditDepartmentData(Department data)
        {


            var EditData = _Context.Department_sk.Where(x => x.DepartmentId == data.DepartmentId).FirstOrDefault();
            if (EditData != null)
            {
                EditData.DepartmentName = data.DepartmentName;
                await _Context.SaveChangesAsync();
                return EditData;

            }
            else
            {
                return null;
            }
        }
    }
}
