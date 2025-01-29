using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet]

       public IActionResult GetAllStudents()
        {

            string[] students = new string[] {"niels","jacob","allen","mia","julieta"};

            return Ok(students);
        }
    }
}
