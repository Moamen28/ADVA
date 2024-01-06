using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Department : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Assuming one manager per department, represented by an Employee
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        // Collection of employees in the department
        public ICollection<Employee> Employees { get; set; }
    }
}
