using FluentValidation;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;
using SingleSignOn.Core.Validators.Validations;

namespace SingleSignOn.Core.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserMessage>
    {
        public CreateUserValidator(IUserRepository userRepo)
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password required");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role required");
            RuleFor(x => x.Username).SetValidator(new DuplicateUsernameValidation(userRepo)).When(x => !string.IsNullOrEmpty(x.Username) && !string.IsNullOrEmpty(x.Password));
        }
    }
}