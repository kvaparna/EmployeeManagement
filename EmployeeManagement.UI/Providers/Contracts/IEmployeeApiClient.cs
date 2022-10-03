using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using System.Collections.Generic;

namespace EmployeeManagement.UI.Providers.Contracts
{
    public interface IEmployeeApiClient
    {
        IEnumerable<EmployeeViewModel> GetEmployees();
        EmployeeDetailedViewModel GetEmployeeById(int Id);
        bool InsertEmployee(EmployeeDetailedViewModel employee);
        bool UpdateEmployee(EmployeeDetailedViewModel employee);
        bool DeleteEmployee(int Id);

    }
}
