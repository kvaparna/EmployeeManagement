using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
  
    public class EmployeeRepository : IEmployeeRepository
    {

        private SqlConnection _sqlConnection;
     

        public EmployeeRepository()
        {
            _sqlConnection = new SqlConnection("data source = (localdb)\\mssqllocaldb; database = TrainingDb;");
        }
        public IEnumerable<EmployeeData> GetEmployees()
        {
              _sqlConnection.Open();

                var sqlCommand = new SqlCommand("exec spGetEmployees", _sqlConnection);

                var sqlDataReader = sqlCommand.ExecuteReader();
                var listOfStudent = new List<EmployeeData>();

                while (sqlDataReader.Read())
                {
                    listOfStudent.Add(new EmployeeData()
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Department_Name = (string)sqlDataReader["Department_Name"]
                     
                    });
                }
            _sqlConnection.Close();

                return listOfStudent;
   
        }
        public EmployeeData GetEmployeeById(int Id)
        {
 
              _sqlConnection.Open();

                var sqlCommand = new SqlCommand("EXEC spGetEmployeeById @Id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);

                var sqlDataReader = sqlCommand.ExecuteReader();

                EmployeeData employee = null;

                while (sqlDataReader.Read())
                {
                    employee = new EmployeeData();
                    employee.Id = (int)sqlDataReader["Id"];
                    employee.Name = (string)sqlDataReader["Name"];
                    employee.Age = (int)sqlDataReader["Age"];
                    employee.Department_Name = (string)sqlDataReader["Department_Name"];
                    employee.Address = (string)sqlDataReader["Address"];
                    employee.Employee_Id = (int)sqlDataReader["Employee_Id"];
            }
            _sqlConnection.Close();

            return employee;
            
        }
        public bool InsertEmployee(EmployeeData employee)
        {
              _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "exec spInsertEmployee @Name,@Age,@Department_Id,@Employee_Id,@Address", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("Department_Id", employee.Department_Id);
                sqlCommand.Parameters.AddWithValue("Employee_Id", employee.Employee_Id);
                sqlCommand.Parameters.AddWithValue("Age", employee.Age);
                sqlCommand.Parameters.AddWithValue("Address", employee.Address);


            sqlCommand.ExecuteNonQuery();

             _sqlConnection.Close();

            return true;           
        }
        public bool UpdateEmployee(EmployeeData employee)
        {
               _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "EXEC spUpdateEmployee @Employee_Id, @Name ,@Age ,@Department_Id ,@Address", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Employee_Id", employee.Employee_Id);
                sqlCommand.Parameters.AddWithValue("Name", employee.Name);
                sqlCommand.Parameters.AddWithValue("Department_Id", employee.Department_Id);
                sqlCommand.Parameters.AddWithValue("Age", employee.Age);
                sqlCommand.Parameters.AddWithValue("Address", employee.Address);
            sqlCommand.ExecuteNonQuery();

            _sqlConnection.Close();


                return true;
            
        }
        public bool DeleteEmployee(int Id)
        {

            _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "EXEC spDeleteEmployee @Id ", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.ExecuteNonQuery();

           _sqlConnection.Close();

                 return true;

        }
    }
}
