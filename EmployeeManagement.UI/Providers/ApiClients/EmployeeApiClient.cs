using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            var response = _httpClient.GetAsync("https://localhost:5001/api/employees").Result;
            var employee = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
            return employee;       
        }

        public EmployeeDetailedViewModel GetEmployeeById(int employeeId)
        {
            //Consume /{employeeId} endpoint in the EmployeeManagementApi using _httpClient
            var response = _httpClient.GetAsync("https://localhost:5001/api/employees/"+ employeeId).Result;
            var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>(response.Content.ReadAsStringAsync().Result);
            return employee;
        }

        public bool InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailedViewModel), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PostAsync("https://localhost:5001/api/insertEmployees", stringContent).Result)
            {
                response.Content.ReadAsStringAsync();
                return true;
            }
        }
        public bool UpdateEmployee(EmployeeDetailedViewModel employeeDetailedViewModel)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeDetailedViewModel), Encoding.UTF8, "application/json");
            using (var response = _httpClient.PostAsync("https://localhost:5001/api/updateEmployees", stringContent).Result)
            {
                //response.Content.ReadAsStringAsync();
                return true;
            }
        }
        public bool DeleteEmployee(int employeeId)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(employeeId));
            using (var response = _httpClient.DeleteAsync("https://localhost:5001/api/deleteEmployees/" + employeeId).Result)
            {
                return true;
            }
            
        }


    }

}
