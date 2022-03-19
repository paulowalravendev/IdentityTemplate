using IdentityTemplate.Api.Features.UserRegister;
using IdentityTemplate.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTemplate.Api.Controllers;

[Route("api/auth")]
public class AuthController : BaseController
{
    private readonly IAuthService _service;
    private readonly IUserService _userService;

    public AuthController(IAuthService service, IUserService userService)
    {
        _service = service;
        _userService = userService;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterInputModel inputModel)
    {
        try
        {
            var userTokenViewModel = await _service.Register(inputModel);
            var userId = await _userService.Register(inputModel, userTokenViewModel!.UserToken.Id);
            userTokenViewModel.UserToken.UserId = userId;
            return CustomResponse(userTokenViewModel);
        }
        catch (CreateUserWithIdentityErrorsException ex)
        {
            return CustomResponseError(ex.Errors);
        }
    }
}