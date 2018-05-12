using SingleSignOn.Core.DTOs;
using SingleSignOn.Core.Installer.Framework;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;
using SingleSignOn.Core.Validators;

namespace SingleSignOn.Core.Mediators.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserMessage, CreateUserResultMessage>
    {
        private readonly IUserRepository _userRepo;
        private readonly CreateUserValidator _validator;

        public CreateUserHandler(IUserRepository userRepo, CreateUserValidator validator)
        {
            _userRepo = userRepo;
            _validator = validator;
        }

        public CreateUserResultMessage Handle(CreateUserMessage message)
        {
            var result = _validator.Validate(message);
            if (result.IsValid)
            {
                _userRepo.Create(message);
            }

            return new CreateUserResultMessage
            {
                Success = result.IsValid,
                ValidationResult = result
            };

        }
    }
}
