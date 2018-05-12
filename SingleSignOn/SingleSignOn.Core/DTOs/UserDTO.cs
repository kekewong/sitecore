using System;
using SingleSignOn.Core.Domain;

namespace SingleSignOn.Core.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public Guid Id { get; set; }
    }
}
