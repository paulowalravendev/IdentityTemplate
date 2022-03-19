using IdentityTemplate.Api.Features.UserRegister;

namespace IdentityTemplate.Api.Services;

public interface IUserService
{
    Task<long> Register(UserRegisterInputModel inputModel, string applicationUserId);
}