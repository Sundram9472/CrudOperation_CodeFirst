using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation_CodeFirst.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name Required!")]
        [MaxLength(80)]
        public String DepartmentName { get; set; }

        //Navigate Properties
        public Employee employees { get; set; }
        

    }
}
