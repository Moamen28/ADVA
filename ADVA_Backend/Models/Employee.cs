using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        // Foreign key for Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // Foreign key for Manager
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        // Collection for employees who this employee is managing
        public ICollection<Employee> Subordinates { get; set; }
    }
}
