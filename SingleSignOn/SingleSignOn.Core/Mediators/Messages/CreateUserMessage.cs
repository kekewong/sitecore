using SingleSignOn.Core.Domain;
using SingleSignOn.Core.Installer.Framework;

namespace SingleSignOn.Core.Mediators.Messages
{
    public class CreateUserMessage : IRequest<CreateUserResultMessage>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
