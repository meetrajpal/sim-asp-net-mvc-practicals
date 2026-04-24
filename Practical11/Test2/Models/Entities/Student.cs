using System;
using System.ComponentModel.DataAnnotations;

namespace Test2.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        [MinLength(10), MaxLength(30)]
        public string FullName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }
    }
}