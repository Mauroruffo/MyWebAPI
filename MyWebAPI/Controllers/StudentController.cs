using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentData _studentData;

        public StudentController(StudentData studentData)
        {
            _studentData = studentData;
        }

        [HttpGet]
        public async Task<IActionResult>List()
        {
            // Create and assign Students list from DB
            List<Student> list = new List<Student>();
            list = await _studentData.StudentList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

    }
}
