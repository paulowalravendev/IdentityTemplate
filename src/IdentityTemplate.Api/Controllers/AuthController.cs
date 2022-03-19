using IdentityTemplate.Api.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IdentityTemplate.Api.Controllers;

[Route("api/auth")]
public class AuthController : BaseController
{
    private readonly AppSettings _appSettings;

    public AuthController(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("hello-world")]
    public ActionResult<string> Hello()
    {
        return Ok("Hello World");
    }
}