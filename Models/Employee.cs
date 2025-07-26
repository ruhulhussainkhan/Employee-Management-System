using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BussinessEmployes.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Employee Name is Required")]
        public string Name { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string City { get; set; }
    }
}