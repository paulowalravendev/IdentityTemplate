namespace IdentityTemplate.Api.Features.UserRegister;

public class UserRegisterInputModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}