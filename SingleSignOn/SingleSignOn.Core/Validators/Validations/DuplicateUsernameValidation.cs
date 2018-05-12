using FluentValidation.Validators;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;

namespace SingleSignOn.Core.Validators.Validations
{
    public class DuplicateUsernameValidation : PropertyValidator
    {
        private readonly IUserRepository _userRepo;

        public DuplicateUsernameValidation(IUserRepository userRepo) : base("Username has been used")
        {
            _userRepo = userRepo;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var msg = context.Instance as CreateUserMessage;

            if (msg == null) return true;

            if (string.IsNullOrEmpty(msg.Username) || string.IsNullOrEmpty(msg.Password)) return true;

            var exists = _userRepo.UsernameExists(msg.Username);

            return !exists;
        }
    }
}