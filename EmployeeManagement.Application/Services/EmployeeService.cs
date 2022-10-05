using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            
                var getEmployees = _employeeRepository.GetEmployees();
                var employee = MapToEmployeeDto(getEmployees);
                return employee;
                     
        }

        private IEnumerable<EmployeeDto> MapToEmployeeDto(IEnumerable<EmployeeData> employeeData)
        {
            var listOfEmployeeDto = new List<EmployeeDto>();
            foreach(var item in employeeData)
            {
                var employeeDto = new EmployeeDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Department_Name = item.Department_Name
                };
                listOfEmployeeDto.Add(employeeDto);
            }
            return listOfEmployeeDto;
    
        }

        public EmployeeDto GetEmployeeById(int Id)
        {
            
                var getEmployeeById = _employeeRepository.GetEmployeeById(Id);
                if(getEmployeeById==null)
                {
                    return null;
                }
                var employee = MapToEmployeeDto(getEmployeeById);
                return employee;      
            
        }

        private EmployeeDto MapToEmployeeDto(EmployeeData employee)
        {
                var employeeDto = new EmployeeDto
                {
                    Id = employee.Id,
                    Name = employee.Name,                    
                    Department_Name = employee.Department_Name,
                    Age = employee.Age,
                    Address=employee.Address,
                    Employee_Id=employee.Employee_Id
                    
                };
  
                return employeeDto;
        }

        public bool InsertEmployee(EmployeeDto employee)
        {

                var insertEmployee = _employeeRepository.InsertEmployee(MapToEmployeeInsert(employee));

                return true;
            
        }

        private EmployeeData MapToEmployeeInsert(EmployeeDto insertEmployee)
        {
            var employeeDto = new EmployeeData()
            {              
                Name = insertEmployee.Name,
                Department_Id = insertEmployee.Department_Id,
                Employee_Id = insertEmployee.Employee_Id,
                Age = insertEmployee.Age,
                Address = insertEmployee.Address
            };
            return employeeDto;
        }

        public bool UpdateEmployee(EmployeeDto employee)
        {
          
                _employeeRepository.UpdateEmployee(MapToEmployeeUpdate(employee));
                return true;
                    
        }

        private EmployeeData MapToEmployeeUpdate(EmployeeDto updateEmployee)
        {
            var employeeDto = new EmployeeData()
            {
                Employee_Id = updateEmployee.Employee_Id,
                Name = updateEmployee.Name,
                Department_Id = updateEmployee.Department_Id,
                Age = updateEmployee.Age,
                Address = updateEmployee.Address
            };
            return employeeDto;
        }

        public bool DeleteEmployee(int Id)
        {
                _employeeRepository.DeleteEmployee(Id);              
                return true;
           
        }
       
    }
}
