using System;
using System.Runtime.Caching;
using NLog;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.DTOs;

namespace SingleSignOn.Core.Services
{
    public interface IAuthCacheService
    {
        void SignIn(UserDTO user);
        UserDTO Check(string username);
    }

    public class AuthCacheService : IAuthCacheService
    {
        private readonly string _prefixKey;
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public AuthCacheService(string prefixKey)
        {
            _prefixKey = prefixKey;
        }

        public void SignIn(UserDTO user)
        {
            var key = _prefixKey + "_" + user.Username;

            MemoryCache.Default.Add(key.ToLower(), user, DateTime.Now.AddMinutes(30));
            _log.Info("Successfully set " + user.Username + " to Auth Cache with key " + key.ToLower());
        }

        public UserDTO Check(string username)
        {
            var key = _prefixKey + "_" + username;
            if (MemoryCache.Default.Contains(key.ToLower()))
            {
                return MemoryCache.Default[key.ToLower()] as UserDTO;
            }

            return null;
        }

    }
}