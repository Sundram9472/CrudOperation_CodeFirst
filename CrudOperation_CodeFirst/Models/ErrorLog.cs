using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation_CodeFirst.Models
{
    public class ErrorLog
    {
        [Key]
        public Guid ErrorId { get; set; }
        [Required]
        public DateTime LoggedOn { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string StackTrace { get; set; }
    }
}
