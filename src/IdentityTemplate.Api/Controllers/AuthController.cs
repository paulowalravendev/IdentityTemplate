using IdentityTemplate.Api.Features.UserRegister;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTemplate.Api.Controllers;

[Route("api/auth")]
public class AuthController : BaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("hello-world")]
    public ActionResult<string> Hello(UserRegisterInputModel inputModel)
    {
        return Ok("Hello World");
    }
}