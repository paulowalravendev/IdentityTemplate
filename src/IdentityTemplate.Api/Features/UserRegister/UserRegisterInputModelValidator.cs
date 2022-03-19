using FluentValidation;

namespace IdentityTemplate.Api.Features.UserRegister;

public class UserRegisterInputModelValidator : AbstractValidator<UserRegisterInputModel>
{
    public UserRegisterInputModelValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(6, 100).WithMessage("{PropertyName} must between 6 and 100 characters.");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("{PropertyName} is invalid.");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(6, 100).WithMessage("{PropertyName} must between 6 and 100 characters.");
    }
}