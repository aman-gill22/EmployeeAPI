using Dapper;
using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }
        public Employee AddEmployee(Employee employee)
        {
            var query = "INSERT INTO Employee (Name, EmployeeCode, DepartmentId) VALUES (@Name, @EmployeeCode, @DepartmentId)" +
            "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("EmployeeCode", employee.EmployeeCode, DbType.String);
            parameters.Add("DepartmentId", employee.DepartmentId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                var id = connection.QuerySingle<int>(query, parameters);
                var createdCompany = new Employee
                {
                    Id = id,
                    Name = employee.Name,
                    EmployeeCode = employee.EmployeeCode,
                    DepartmentId = employee.DepartmentId
                };
                return createdCompany;
            }
        }

        public void DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, new { id });
            }
        }

        public List<Employee> GetAllEmployees()
        {
            var query = "SELECT * FROM Employee";
            using (var connection = _context.CreateConnection())
            {
                var employees = connection.Query<Employee>(query);
                return employees.ToList();
            }
        }

        public Employee GetEmployee(int id)
        {
            var query = "SELECT * FROM Employee WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var employee = connection.QuerySingleOrDefault<Employee>(query, new { id });
                return employee;
            }
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            var query = "UPDATE Employee SET Name = @Name, DepartmentId = @DepartmentId WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("DepartmentId", employee.DepartmentId, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                 connection.Execute(query, parameters);
            }
        }
    }
}
