using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Models;

namespace CrudOperation_CodeFirst.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base (options)
        {

        }
        public DbSet<Department>Department_sk { get; set; }
        public DbSet<Employee> Employee_sk { get; set; }

        public DbSet<ErrorLog> ErrorLog { get; set; }


    }
}
