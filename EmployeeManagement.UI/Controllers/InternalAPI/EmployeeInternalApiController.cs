using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.UI.Controllers.InternalAPI
{
    [Route("api/internal")]
    [ApiController]
    public class EmployeeInternalApiController : ControllerBase
    {
        private readonly IEmployeeApiClient _employeeApiClient;

        public EmployeeInternalApiController(IEmployeeApiClient employeeApiClient)
        {
            _employeeApiClient = employeeApiClient;
        }
        [HttpGet]
        [Route("employees")]
        public IActionResult GetEmployees()
        {
            try
            {
                var employee = _employeeApiClient.GetEmployees();

                return Ok(employee);

            }
            catch (Exception exception)
            {
                throw;

            }
        }


        [HttpGet]
        [Route("employees/{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        {
            try
            {
                var employee = _employeeApiClient.GetEmployeeById(employeeId);

                return Ok(employee);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        
        [HttpPost]
        [Route("insertEmployees")]

        public IActionResult InsertEmployee([FromBody] EmployeeDetailedViewModel employees)
        {
            try
            {              
                var result = _employeeApiClient.InsertEmployee(employees);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateEmployees/{employeeId}")]

        public IActionResult UpdateEmployee([FromRoute] int employeeId,[FromBody] EmployeeDetailedViewModel employees)
        {
            try
            {
                var result = _employeeApiClient.UpdateEmployee(employees);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteEmployees/{employeeId}")]

        public IActionResult DeleteEmployee([FromRoute] int employeeId)
        {
            try
            {
                var result = _employeeApiClient.DeleteEmployee(employeeId);
                return Ok(result);
            }
           
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
