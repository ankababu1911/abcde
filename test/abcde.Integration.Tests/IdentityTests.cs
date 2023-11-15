using abcde.Client.Services.Interfaces;
using abcde.Integration.Tests.Base;
using abcde.Test.Data;
using abcde.Model.Exceptions;
using abcde.Model.Identity;
using abcde.Client;

namespace abcde.Integration.Tests
{
    /// <summary>
    /// Integration tests for IdentityService
    /// </summary>
    public class IdentityTests : BaseIntegrationTestUseSqliteProgram
    {
        private readonly IIdentityService _sut;

        public IdentityTests()
        {
            _sut = APIGateway.IdentityService;
        }

        [Fact]
        public async Task Should_VerifyOrganisation()
        {
            // Arrange
            var model = IdentityData.GetVerifyOrganisation();
            model.EncryptedOrganisationId = Guid.NewGuid().ToString();

            // Act
            var result = await _sut.VerifyOrganisation(model);

            // Assert
            Assert.NotNull(result);

            var assertRegisterUser = await _sut.Login(new LoginModel() { Email = model.EmailId, Password = model.Password });
            Assert.NotNull(assertRegisterUser);
            Assert.True(assertRegisterUser.Successful);
            Assert.NotNull(assertRegisterUser.UserId);
            Assert.NotNull(assertRegisterUser.Token);

            var assertTenant = await APIGateway.TenantService.GetAsyncString(model.EncryptedOrganisationId);
            Assert.NotNull(assertTenant);

            var assertTenantSettings = await APIGateway.TenantSettingsService.GetAsyncString(model.EncryptedOrganisationId);
            Assert.NotNull(assertTenantSettings);
            Assert.Equal("New Org", assertTenantSettings.Name);
            Assert.Equal("email@neworg.com", assertTenantSettings.ContactEmail);
            Assert.Equal("Mr Contact Name", assertTenantSettings.ContactName);
        }

        [Fact]
        public async Task Should_Not_VerifyOrganisation_No_Organisation()
        {
            // Arrange
            var model = IdentityData.GetVerifyOrganisation();

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ClientException>(async () => await _sut.VerifyOrganisation(model));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ClientException>(ex);
            Assert.Equal("UnableToValidate", ex.Message);
        }

        [Fact]
        public async Task Should_Register()
        {
            // Arrange
            var registerModel = IdentityData.GetRegisterModel();

            // Act
            var result = await _sut.Register(registerModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Successful);
        }

        [Fact]
        public async Task Should_Register_With_Tenant()
        {
            // Arrange
            var tenant = await APIGateway.TenantService.GetFirstAsync();
            var registerModel = IdentityData.GetRegisterModel(tenant.TenantId.ToString());

            // Act
            var result = await _sut.Register(registerModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Successful);
        }

        [Fact]
        public async Task Should_Login()
        {
            // Arrange
            var existingLoginAudit = await APIGateway.LoginAuditService.GetAllAsync();

            var registerModel = new RegisterModel()
            {
                Password = "!+Password1",
                ConfirmPassword = "!+Password1",
                Email = "test@test.com"
            };
            await _sut.Register(registerModel);

            // Act
            var result = await _sut.Login(new LoginModel()
            {
                Email = "test@test.com",
                Password = "!+Password1"
            });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Successful);
            Assert.NotNull(result.Token);

            var audit = await APIGateway.LoginAuditService.GetAllAsync();
            Assert.NotEmpty(audit);
            Assert.NotEqual(existingLoginAudit.Count(), audit.Count());
        }

        [Fact]
        public async Task Should_Login_SystemAdmin()
        {
            // Arrange
            
            // Act
            var result = await _sut.Login(new LoginModel()
            {
                Email = "systemadmin@abcde.com",
                Password = "!+Password1"
            });

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Successful);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task Should_ChangePassword()
        {
            // Arrange
            var registerModel = new RegisterModel()
            {
                Password = "!+Password1",
                ConfirmPassword = "!+Password1",
                Email = "test@test.com"
            };
            await _sut.Register(registerModel);

            var loginResult = await _sut.Login(new LoginModel()
            {
                Email = "test@test.com",
                Password = "!+Password1"
            });

            // Act
            var result = await _sut.ChangePassword(new ChangePasswordModel()
            {
                UserId = loginResult.UserId,
                CurrentPassword = "!+Password1",
                Password = "!+Password2",
                ConfirmPassword = "!+Password2"
            });

            // Assert
            Assert.NotNull(result);

            var assertloginResult = await _sut.Login(new LoginModel() { Email = "test@test.com", Password = "!+Password2" });
            Assert.NotNull(assertloginResult);
            Assert.True(assertloginResult.Successful);
            Assert.NotNull(assertloginResult.UserId);
            Assert.NotNull(assertloginResult.Token);
        }

        [Fact]
        public async Task Should_Fail_Register_Passwords_Dont_Match()
        {
            // Arrange
            var registerDto = IdentityData.GetRegisterDetailsPasswordDontMatch();

            // Act
            var ex = await Record.ExceptionAsync(() => _sut.Register(registerDto));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ClientException>(ex);
        }

        [Fact]
        public async Task Should_Fail_Register_Passwords_Not_Strong()
        {
            // Arrange
            var registerDto = IdentityData.GetRegisterDetailsPasswordDontMatch();

            // Act
            var ex = await Record.ExceptionAsync(() => _sut.Register(registerDto));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ClientException>(ex);
        }

        [Fact]
        public async Task Should_Fail_Register_Passwords_Missing_Async()
        {
            // Arrange
            var registerDto = IdentityData.GetRegisterDetailsPasswordMissing();

            // Act
            var ex = await Record.ExceptionAsync(() => _sut.Register(registerDto));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<ClientException>(ex);
        }
    }
}
