using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DepartmentCreateDto
    {
        public string Name { get; set; }
        public int? ManagerId { get; set; }
    }
}
