using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeData _employeeData;

        public EmployeeController(EmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Employee> list = await _employeeData.EmployeeList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            Employee emp = await _employeeData.GetEmployee(id);
            return StatusCode(StatusCodes.Status200OK, emp);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee emp)
        {
            bool resp = await _employeeData.CreateEmployee(emp);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = resp});
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] Employee emp)
        {
            bool resp = await _employeeData.EditEmployee(emp);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            bool resp = await _employeeData.DeleteEmployee(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }

    }
}
