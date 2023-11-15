using abcde.Client;
using abcde.Integration.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace abcde.Integration.Tests.ApiTests
{
    public class LoginTest
    {
        //create a test for login with attributes of username and password passed above method and check if it returns a token
        [Theory]
        [Xunit.InlineData("Barbara_Carter@yahoo.com", "Abcd@1234", "AB56D4", true)]
        [Xunit.InlineData("Tyrone_Pagac@gmail.com", "Abcd@1234", "", false)]
        [Xunit.InlineData("Vernon33@yahoo.com", "Abcd@1234", "", true)]
        [Xunit.InlineData("Howard.Wolf17@hotmail.com", "Abcd@1234", "AB56D4", false)]
        public async Task LoginTest1(string username, string password, string connectionStringCode, bool successResult)
        {
            // Arrange
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7053/api/v1/");

            APIGateway apiGateway = new APIGateway(client);
            if (!string.IsNullOrEmpty(connectionStringCode))
            {
                apiGateway.SetConnectionStringCode(connectionStringCode);
            }
            Model.Identity.LoginResult result = null;
            // Act
            try
            {
                result = await apiGateway.IdentityService.Login(new Model.Identity.LoginModel() { Email = username, Password = password });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = new Model.Identity.LoginResult();
            }
            // Assert
            Assert.NotNull(result);
            if (successResult)
            {
                Assert.True(!string.IsNullOrEmpty(result.Token), $"Issue with username = {username} and password= {password} connectionStringCode= {connectionStringCode} and token= {result.Token}");
                Assert.True(result.UserId != Guid.Empty, $"Issue with username = {username} and password= {password} connectionStringCode= {connectionStringCode} and userId= {result.UserId}");
            }
            else
            {
                Assert.False(!string.IsNullOrEmpty(result.Token), $"Issue with username = {username} and password= {password} connectionStringCode= {connectionStringCode} and token= {result.Token}");
                Assert.False(result.UserId != Guid.Empty, $"Issue with username = {username} and password= {password} connectionStringCode= {connectionStringCode} and userId= {result.UserId}");
            }
        }
    }
}