using System;
using System.Runtime.Caching;
using FakeItEasy;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.DTOs;
using SingleSignOn.Core.Mediators.Handlers;
using SingleSignOn.Core.Mediators.Messages;
using SingleSignOn.Core.Services;
using SingleSignOn.Core.Validators;

namespace SingleSignOn.Test
{
    [TestClass]
    public class CreateUserHandlerTest
    {
        [TestMethod]
        public void DataMustBeSavedToDatabaseOnceSuccessfulPassValidation()
        {
            var fakeUserRepo = A.Fake<IUserRepository>();
            var fakeValidator = new CreateUserValidator(fakeUserRepo);
            
            var testMessage = new CreateUserMessage
            {
                Username = "Test",
                Password = "Test",
                Role = UserRole.Admin
            };
            var handler = new CreateUserHandler(fakeUserRepo, fakeValidator);

            handler.Handle(testMessage);

            A.CallTo(() => fakeUserRepo.Create(testMessage)).MustHaveHappened();
        }

    }
}
