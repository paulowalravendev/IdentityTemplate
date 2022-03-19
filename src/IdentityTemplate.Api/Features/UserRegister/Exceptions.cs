namespace IdentityTemplate.Api.Features.UserRegister;

public class CreateUserWithIdentityErrorsException : Exception
{
    public CreateUserWithIdentityErrorsException(IEnumerable<string> errors)
    {
        Errors = errors;
    }
    public IEnumerable<string> Errors { get; }
}