using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.DTOs;
using SingleSignOn.Core.Services;

namespace SingleSignOn.Test
{
    [TestClass]
    public class AuthCacheServiceTest
    {
        [TestMethod]
        public void MemoryCacheMustHaveDataAfterSignIn()
        {
            var prefixKey = "AuthCache";
            var service = new AuthCacheService(prefixKey);
            var newUser = new UserDTO
            {
                Id = Guid.NewGuid(),
                Username = "Test456",
                Role = UserRole.Admin
            };
            service.SignIn(newUser);

            var key = prefixKey +"_"+ newUser.Username;
            MemoryCache.Default.Contains(key.ToLower()).ShouldBeTrue();

            var memoryUserData = MemoryCache.Default[key.ToLower()] as UserDTO;
            memoryUserData.Username.ShouldBe(newUser.Username);
            memoryUserData.Role.ToString().ShouldBe(newUser.Role.ToString());
            memoryUserData.Id.ToString().ShouldBe(newUser.Id.ToString());
        }

        [TestMethod]
        public void CheckMethodShouldReturnDataIfUsernameIsValid()
        {
            var prefixKey = "AuthCache";
            var service = new AuthCacheService(prefixKey);
            var newUser = new UserDTO
            {
                Id = Guid.NewGuid(),
                Username = "Test123",
                Role = UserRole.Admin
            };
            service.SignIn(newUser);

            var memoryUserData = service.Check(newUser.Username);
            memoryUserData.Username.ShouldBe(newUser.Username);
            memoryUserData.Role.ToString().ShouldBe(newUser.Role.ToString());
            memoryUserData.Id.ShouldBe(newUser.Id);
        }

    }
}
