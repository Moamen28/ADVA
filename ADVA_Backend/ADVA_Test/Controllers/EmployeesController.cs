#region NameSpaces
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Roposityres.Interfaces;

#endregion
namespace ADVA_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Inject Repositorys and unitOfWork to the Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IModelRepository<Department> _deptrepo;
        private readonly IModelRepository<Employee> _emprepo;
        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper= mapper;
            _deptrepo = _unitOfWork.GetDepartmentRepo();
            _emprepo = _unitOfWork.GetEmployeeRepo();
        }
        #endregion


        #region Get All Employees
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {

            try
            {
                // Await the Task to get the all employee with mange name 
                var employeesQuery = await _emprepo.GetAllIncludingAsync(e => e.Manager);

                // Now apply ToListAsync to the result
                var employees = await employeesQuery.ToListAsync();

                // Map Employees EmployeeDto
                var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                return Ok(employeeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Add Employee
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeCreateDTO employeeDto)
        {

            if (employeeDto == null)
            {
                return BadRequest("Employee data must be provided.");
            }

            try
            {
                // Use AutoMapper to map DTO to Entity
                var employee = _mapper.Map<Employee>(employeeDto);

                // Add the new employee
                await _emprepo.AddAsync(employee);
                await _unitOfWork.Save();

                var employeeReadDto = _mapper.Map<EmployeeDto>(employee);
                // Return the newly created employee
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employeeReadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
            
        
        #endregion

        #region Get Employee By Id

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _emprepo.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return Ok(employee);
        }
        #endregion

        #region Remove Employee
        [HttpDelete]
        [Route("RemoveEmployee/{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            try
            {
                var employee = await _emprepo.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                // Remove the employee
                await _emprepo.RemoveAsync(employee);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Update Employee
        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null || id != employeeDto.Id)
            {
                return BadRequest("Invalid employee data.");
            }

            try
            {
                var employee = await _emprepo.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                // Use AutoMapper to update the employee's properties
                _mapper.Map(employeeDto, employee);

                await _emprepo.UpdateAsync(employee);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, ex.Message);
            }
        } 
        #endregion
    }
}