using FluentValidation.Results;
using SingleSignOn.Core.DTOs;

namespace SingleSignOn.Core.Mediators.Messages
{
    public class CreateUserResultMessage
    {
        public bool Success { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}