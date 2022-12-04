using EmployeeAPI.Models;
using EmployeeAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var employees = _employeeRepo.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}", Name = "EmployeeById")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = _employeeRepo.GetEmployee(id);
                if (employee == null)
                    return NotFound();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            try
            {
                employee.EmployeeCode = Guid.NewGuid().ToString();
                var createdEmployee =  _employeeRepo.AddEmployee(employee);
                return CreatedAtRoute("EmployeeById", new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                var dbEmployee = _employeeRepo.GetEmployee(id);
                if (dbEmployee == null)
                    return NotFound();
                _employeeRepo.UpdateEmployee(id, employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dbEmployee =  _employeeRepo.GetEmployee(id);
                if (dbEmployee == null)
                    return NotFound();
                 _employeeRepo.DeleteEmployee(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
