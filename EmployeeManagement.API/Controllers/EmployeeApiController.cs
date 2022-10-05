using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("employees")]
        public IActionResult GetEmployees()
        {
            
            try
            {

                var getEmployees = _employeeService.GetEmployees();
                return Ok(MapToEmployeeDetailedViewModel(getEmployees));
            }
            catch (Exception exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
            
        }

        private object MapToEmployeeDetailedViewModel(IEnumerable<EmployeeDto> getEmployees)
        {
            var listOfEmployeeDetailedViewModel = new List<EmployeeDetailedViewModel>();
            foreach (var item in getEmployees)
            {
                var employeeDetailedViewModel = new EmployeeDetailedViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Department_Name = item.Department_Name
                };
                listOfEmployeeDetailedViewModel.Add(employeeDetailedViewModel);
            }
            return listOfEmployeeDetailedViewModel;
        }      

        [HttpGet]
        [Route("employees/{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employeeById = _employeeService.GetEmployeeById(employeeId);
                if(employeeById==null)
                {
                    return NotFound();
                }
                return Ok(MapToEmployeeDetailedViewModel(employeeById));
                
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private object MapToEmployeeDetailedViewModel(EmployeeDto employeeById)
        {
            var employeeDto = new EmployeeDto
            {
                Id = employeeById.Id,
                Name = employeeById.Name,
                Department_Name = employeeById.Department_Name,
                Age = employeeById.Age,
                Address = employeeById.Address,
                Employee_Id = employeeById.Employee_Id

            };

            return employeeDto;
        }

        [HttpPost]
        [Route("insertEmployees")]
        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel employee)
        {
            try
            {           
                var insertEmployee = _employeeService.InsertEmployee(MapToEmployeeInsert(employee));
                if(insertEmployee)
                {
                    return Ok(insertEmployee);
                }
                else 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private EmployeeDto MapToEmployeeInsert(EmployeeDetailedViewModel employee)
        {
            var employeeDto = new EmployeeDto()
            {
                
                Name = employee.Name,
                Department_Id = employee.Department_Id,
                Employee_Id = employee.Employee_Id,
                Age = employee.Age,
                Address = employee.Address
            };
            return employeeDto;
        }

        

        [HttpPut]
        [Route("updateEmployees")]

        public IActionResult UpdateStudent([FromBody] EmployeeDetailedViewModel employee)
        {
            try
            {
              
                var UpdateEmployee = _employeeService.UpdateEmployee(MapToEmployeeUpdate(employee));
                if(UpdateEmployee)
                {
                    return Ok(UpdateEmployee);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update employee details");
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private EmployeeDto MapToEmployeeUpdate(EmployeeDetailedViewModel employee)
        {
            var employeeDto = new EmployeeDto()
            {
                Id = employee.Id,
                Employee_Id = employee.Employee_Id,
                Name = employee.Name,
                Department_Id = employee.Department_Id,
                Age = employee.Age,
                Address = employee.Address
            };
            return employeeDto;
        }

        [HttpDelete]
        [Route("deleteEmployees/{employeeId}")]

        public IActionResult DeleteStudent([FromRoute] int employeeId)
        {
            try
            {              
                var employee = _employeeService.GetEmployeeById(employeeId);
                

                var result = _employeeService.DeleteEmployee(employeeId);
                return Ok(result);
            }
           
            
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
