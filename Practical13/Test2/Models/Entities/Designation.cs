using System.Collections.Generic;

namespace Test2.Models.Entities
{
    public class Designation
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }

}