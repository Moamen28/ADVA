using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class EmployeeCreateDTO
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
    }
}

