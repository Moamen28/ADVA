using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Roposityres.Interfaces;

namespace ADVA_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Inject Repositorys and unitOfWork to the Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelRepository<Department> _deptrepo;
        private readonly IModelRepository<Employee> _emprepo;
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _deptrepo = _unitOfWork.GetDepartmentRepo();
            _emprepo= _unitOfWork.GetEmployeeRepo();
        }
        #endregion

        #region Get All Department
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                // Await the asynchronous operation and then apply the Select
                var departments = await _deptrepo.GetAllIncludingAsync(d => d.Employees, d => d.Manager);
                var departmentDtos = departments.Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    ManagerName = d.Manager.Name, 
                    Employees = d.Employees.Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Salary = e.Salary,
                        ManagerId = e.ManagerId,
                        ManagerName = e.Manager.Name
                    }).ToList()
                }).ToListAsync(); 

                return Ok(await departmentDtos); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Add New Department
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentCreateDto departmentDto)
        {
            if (departmentDto == null || string.IsNullOrWhiteSpace(departmentDto.Name))
            {
                return BadRequest("Department information is not provided or invalid.");
            }

            try
            {
                var department = new Department
                {
                    Name = departmentDto.Name,
                    ManagerId = departmentDto.ManagerId 
                };

                await _deptrepo.AddAsync(department);
                await _unitOfWork.Save(); 

                // This line to return the new department created 
                return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        #endregion

        #region Get epartment by ID
        [HttpGet]
        [Route("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                // This is used to include Employees with the department
                var department = await _deptrepo.GetByIdIncludingAsync(id, d => d.Employees);

                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }

                #region Comment This Can be used if we want to retrun specific fileds

                //var departmentDto = new DepartmentDto
                //{
                //    Id = department.Id,
                //    Name = department.Name,
                //    Employees = department.Employees.Select(e => new EmployeeDto
                //    {
                //        Id = e.Id,
                //        Name = e.Name,
                //        Salary = e.Salary,
                //        ManagerId = e.ManagerId

                //    }).ToList()

                //}; 
                #endregion

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Remove Department
        [HttpDelete]
        [Route("RemoveDepartmentById/{id}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            try
            {
                // Find the department by id
                var department = await _deptrepo.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }

                // Remove the department
                await _deptrepo.RemoveAsync(department);
                await _unitOfWork.Save();

                // Return a success message or NoContent
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Update Department
        [HttpPut]
        [Route("UpdateDepartmentById/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentCreateDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Invalid department data.");
            }

            try
            {
                var department = await _deptrepo.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }
                // If a ManagerId is provided, validate it exists as an employee
                if (departmentDto.ManagerId.HasValue)
                {
                    var managerExists = await _emprepo.GetByIdAsync(departmentDto.ManagerId.Value) != null;
                    if (!managerExists)
                    {
                        return BadRequest("Manager ID does not correspond to a valid employee.");
                    }
                }
                // Map the updated fields from the DTO to the existing department 
                department.Name = departmentDto.Name;
                department.ManagerId = departmentDto.ManagerId;

                // Update the department
                await _deptrepo.UpdateAsync(department);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 
        #endregion
    }
}
