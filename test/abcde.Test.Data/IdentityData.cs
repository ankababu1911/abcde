using Microsoft.AspNetCore.Identity;
using abcde.Data;
using abcde.Model;
using abcde.Model.Identity;
using abcde.Model.Identity.DTOs;
using abcde.Test.Data.Base;
using System.Data;

namespace abcde.Test.Data
{
    public class IdentityData : BaseData
    {
        public static AuthenticateDto Get()
        {
            return new AuthenticateDto()
            {
                Email = new Random().Next(2000) + "@test.com",
                UserId = Guid.NewGuid().ToString()
            };
        }

        public static RegisterModel GetRegisterDetails(string role)
        {
            var password = "Password1";

            return new RegisterModel()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = new Random().Next(2000) + "@test.com",
                Password = password,
                ConfirmPassword = password
            };
        }

        public static RegisterModel GetRegisterDetailsPasswordDontMatch()
        {
            // Arrange
            var password = "!+Password1";
            var passwordOther = "!+Password2";

            return new RegisterModel()
            {
                Email = new Random().Next(2000) + "@test.com",
                Password = password,
                ConfirmPassword = passwordOther,
            };
        }

        public static RegisterModel GetRegisterDetailsPasswordNotStrong()
        {
            // Arrange
            var password = "123456";
            var passwordOther = "123456";

            return new RegisterModel()
            {
                Email = new Random().Next(2000) + "@test.com",
                Password = password,
                ConfirmPassword = passwordOther,
            };
        }

        public static RegisterModel GetRegisterDetailsPasswordMissing()
        {
            // Arrange
            var password = "!+Password1";
            var passwordOther = Guid.NewGuid().ToString();

            return new RegisterModel()
            {
                Email = new Random().Next(2000) + "@test.com",
                Password = password,
            };
        }

        public static AuthenticateDto GetUserDetailsPasswordMissing()
        {
            return new AuthenticateDto()
            {
                Email = new Random().Next(2000) + "@test.com",
                Password = null,
            };
        }

        public static IEnumerable<User> GetEnumerable()
        {
            return new[]
            {
                new User()
                {
                    Email = Guid.NewGuid().ToString(),
                    Datestamp = DateTime.Now.AddDays(0),
                    UserId = Guid.NewGuid().ToString(),
                },
                new User
                {
                    Email = Guid.NewGuid().ToString(),
                    Datestamp = DateTime.Now.AddDays(0),
                    UserId = Guid.NewGuid().ToString(),
                },
                new User
                {
                    Email = Guid.NewGuid().ToString(),
                    Datestamp = DateTime.Now.AddDays(0),
                    UserId = Guid.NewGuid().ToString(),
                }
            }.ToArray();
        }

        public static RegisterModel GetRegisterModel(string? tenantId = null)
        {
            var password = "!+Password1";

            return new RegisterModel()
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = new Random().Next(2000) + "@test.com",
                Password = password,
                ConfirmPassword = password,
                TenantId = tenantId,
                Role = Roles.SystemAdmin.ToString()
            };
        }

        public static VerifyOrganisation GetVerifyOrganisation()
        {
            return new VerifyOrganisation()
            {
                EmailId = new Random().Next(2000) + "@test.com",
                Password = "!+Password1",
            };
        }
    }
}
