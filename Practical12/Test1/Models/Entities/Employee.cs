using System;
using System.ComponentModel.DataAnnotations;

namespace Test1.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }


        public string Address { get; set; }
    }
}