using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roposityres.Interfaces
{
    public interface IUnitOfWork
    {
        Task Save();
        IModelRepository<Employee> GetEmployeeRepo();
        IModelRepository<Department> GetDepartmentRepo();
    }
}
