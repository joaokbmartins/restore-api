using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

  [ApiController]
  [Route("api/v1/[controller]")]
  public class TestController : ControllerBase
  {

    [HttpGet("not-found")]
    public ActionResult GetNotFound()
    {
      return NotFound();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
      ProblemDetails problem = new ProblemDetails();
      problem.Title = "Bad request received.";
      return BadRequest(problem);
    }

    [HttpGet("unauthorized")]
    public ActionResult GetUnathorized()
    {
      return Unauthorized();
    }

    [HttpGet("validation-error")]
    public ActionResult GetValidationError()
    {
      ModelState.AddModelError("Error 1: ", "Some error.");
      ModelState.AddModelError("Error 2: ", "Somme other error");
      return ValidationProblem();
    }

    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
      throw new Exception("Server error.");
    }

  }
}