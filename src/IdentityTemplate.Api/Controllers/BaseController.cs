using Microsoft.AspNetCore.Mvc;

namespace IdentityTemplate.Api.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    private readonly List<string> _errors = new();

    protected ActionResult CustomResponse(object? result = null)
    {
        if (_errors.Any())
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Messages", _errors.ToArray()}
            }));

        return Ok(result);
    }

    protected ActionResult CustomResponseError(string error)
    {
        _errors.Add(error);
        return CustomResponse();
    }
    protected ActionResult CustomResponseError(IEnumerable<string> error)
    {
        _errors.AddRange(error);
        return CustomResponse();
    }

    protected void AddError(string error)
    {
        _errors.Add(error);
    }
}