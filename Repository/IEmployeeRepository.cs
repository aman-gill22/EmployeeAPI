using EmployeeAPI.Models;

namespace EmployeeAPI.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        void UpdateEmployee(int id, Employee employee);
        Employee AddEmployee(Employee employee);
        void DeleteEmployee(int id);

    }
}
