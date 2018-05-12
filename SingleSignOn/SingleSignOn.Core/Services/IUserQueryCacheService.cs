using System;
using System.Runtime.Caching;
using SingleSignOn.Core.Domain;

namespace SingleSignOn.Core.Services
{
    public interface IUserQueryCacheService
    {
        User GetOrSet(string username, Func<User> getUserCallback);
        void ClearCollectionObjectCache(string username);
    }

    public class UserQueryCacheService : IUserQueryCacheService
    {
        private readonly string _prefixKey;

        public UserQueryCacheService(string prefixKey)
        {
            _prefixKey = prefixKey;
        }

        public User GetOrSet(string username, Func<User> getUserCallback)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            var key = _prefixKey + "_" + username;

            if (MemoryCache.Default.Contains(key.ToLower()))
            {
                return MemoryCache.Default.Get(key.ToLower()) as User;
            }

            var queryUser = getUserCallback();
            if (queryUser != null)
            {
                MemoryCache.Default.Add(key.ToLower(), queryUser, DateTime.Now.AddMinutes(60));
            }
            return queryUser;
        }

        public void ClearCollectionObjectCache(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return;
            }
            
            var key = _prefixKey + "_" + username;
            if (MemoryCache.Default.Contains(key.ToLower()))
            {
                MemoryCache.Default.Remove(key.ToLower());
            }
        }
    }
}