using System;
using System.ComponentModel.DataAnnotations;

namespace Test1.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        public int? Age { get; set; }
    }
}