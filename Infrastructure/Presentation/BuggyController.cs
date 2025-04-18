using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")] // GET : /api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(); // 404
        }

        [HttpGet("servererror")] // GET : /api/buggy/servererror
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception(); 
            return Ok(); 
        }

        [HttpGet("badrequest")] // GET : /api/buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(); // 400
        }

        [HttpGet("badrequest/{id}")] // GET : /api/buggy/badrequest/ahmed
        public IActionResult GetBadRequest(int id) // Validation Error
        {
            return BadRequest(); // 400
        }

        [HttpGet("unauthorized")] // GET : /api/buggy/notfound
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized(); // 401
        }



    }
}
