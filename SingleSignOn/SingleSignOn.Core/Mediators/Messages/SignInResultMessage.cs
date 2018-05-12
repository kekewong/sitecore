using FluentValidation.Results;
using SingleSignOn.Core.DTOs;

namespace SingleSignOn.Core.Mediators.Messages
{
    public class SignInResultMessage
    {
        public bool IsSignInByCached { get; set; }
        public bool Success { get; set; }
        public UserDTO User { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}