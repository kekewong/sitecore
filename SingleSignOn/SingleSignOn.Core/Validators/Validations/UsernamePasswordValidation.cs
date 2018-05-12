using FluentValidation.Validators;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;

namespace SingleSignOn.Core.Validators.Validations
{
    public class UsernamePasswordValidation : PropertyValidator
    {
        private readonly IUserRepository _userRepo;

        public UsernamePasswordValidation(IUserRepository userRepo) : base("Invalid Username and Password")
        {
            _userRepo = userRepo;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var msg = context.Instance as SignInMessage;

            if (msg == null) return true;

            if (string.IsNullOrEmpty(msg.Username) || string.IsNullOrEmpty(msg.Password)) return true;

            var user = _userRepo.Get(msg.Username, msg.Password);

            return user != null;
        }
    }
}