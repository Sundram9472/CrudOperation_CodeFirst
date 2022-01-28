using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation_CodeFirst.Models
{
    public class Employee
    {
      
        public int DepartmentId { get; set; }
       
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name Required!")]
        [MaxLength(80)]
        public String EmployeeName { get; set; }

        [Required(ErrorMessage = "Gmail Required!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public String EmployeeGmail { get; set; }

        [Required(ErrorMessage = "Employee Contact Required!")]
        [MaxLength(80)]
        public String EmployeeContact { get; set; }

        [Required(ErrorMessage = "Employee Adress Required!")]
        [MaxLength(80)]
        public String EmployeeAdress { get; set; }

    }
}
