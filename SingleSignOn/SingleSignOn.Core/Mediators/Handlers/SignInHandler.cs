using SingleSignOn.Core.DTOs;
using SingleSignOn.Core.Installer.Framework;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;
using SingleSignOn.Core.Validators;

namespace SingleSignOn.Core.Mediators.Handlers
{
    public class SignInHandler : IRequestHandler<SignInMessage, SignInResultMessage>
    {
        private readonly IAuthCacheService _authCacheService;
        private readonly IUserRepository _userRepo;
        private readonly SignInValidator _validator;

        public SignInHandler(IAuthCacheService authCacheService, IUserRepository userRepo, SignInValidator validator)
        {
            _authCacheService = authCacheService;
            _userRepo = userRepo;
            _validator = validator;
        }

        public SignInResultMessage Handle(SignInMessage message)
        {
            var result = _validator.Validate(message);
            if (result.IsValid)
            {
                var cachedUser = _authCacheService.Check(message.Username);
                if (cachedUser != null)
                {
                    return new SignInResultMessage
                    {
                        User = new UserDTO
                        {
                            Role = cachedUser.Role,
                            Username = cachedUser.Username,
                            Id = cachedUser.Id,
                        },
                        Success = result.IsValid,
                        ValidationResult = result,
                        IsSignInByCached = true
                    };
                }

                var user = _userRepo.Get(message.Username, message.Password);
                _authCacheService.SignIn(new UserDTO { Id = user.Id, Role = user.Role, Username = user.Username });

                return new SignInResultMessage
                {
                    User = new UserDTO
                    {
                        Role = user.Role,
                        Username = user.Username,
                        Id = user.Id,
                    },
                    Success = result.IsValid,
                    ValidationResult = result,
                    IsSignInByCached = false
                };
            }

            return new SignInResultMessage
            {
                Success = result.IsValid,
                ValidationResult = result
            };

        }
    }
}
