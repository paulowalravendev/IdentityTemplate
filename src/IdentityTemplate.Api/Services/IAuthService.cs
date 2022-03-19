using IdentityTemplate.Api.Features;
using IdentityTemplate.Api.Features.UserRegister;

namespace IdentityTemplate.Api.Services;

public interface IAuthService
{
    Task<UserTokenViewModel?> Register(UserRegisterInputModel inputModel);
}