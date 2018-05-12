using SingleSignOn.Core.Installer.Framework;

namespace SingleSignOn.Core.Mediators.Messages
{
    public class SignInMessage : IRequest<SignInResultMessage>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
