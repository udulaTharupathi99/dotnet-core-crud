using CrudDemo.Data;
using CrudDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(repository.GetAllEmployees());
        }


        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var emp = repository.GetEmployeeById(id);

            if (emp != null)
            {
                return Ok(emp);
            }
            return NotFound("not found");
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            repository.AddEmployee(employee);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = repository.GetEmployeeById(id);
            if(employee != null)
            {
                repository.DeleteEmployee(employee);
                return Ok(id);
                
            }
            return NotFound("not found");

        }

        [HttpPut("{id}")]
        public IActionResult EditEmployee(int id, Employee employee)
        {
            var oldemp = repository.GetEmployeeById(id);
            if(oldemp != null)
            {
                employee.Id = oldemp.Id;
                repository.EditEmployee(employee);
            }
            return Ok(employee);
        }

    }
}
