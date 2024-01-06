
using DataAccess;
using Models;
using Roposityres.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roposityres.Repository
{
    public class unitOfWork : IUnitOfWork
    {
        #region Inject Repositorys
        private IModelRepository<Employee> Emprepo;
        private IModelRepository<Department> DeptRepo;

        private ADVADbContext Context;
        public unitOfWork(IModelRepository<Employee> _emprepo, ADVADbContext _context, IModelRepository<Department> _deptrepo)
        {
            Emprepo = _emprepo;
            Context = _context;
            DeptRepo = _deptrepo;
        } 
        #endregion

        public IModelRepository<Department> GetDepartmentRepo()
        {
            return DeptRepo;
        }

        public IModelRepository<Employee> GetEmployeeRepo()
        {
           return Emprepo;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}