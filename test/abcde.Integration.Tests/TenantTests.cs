using abcde.Client.Services.Interfaces;
using abcde.Integration.Tests.Base;

namespace abcde.Integration.Tests
{
    public class TenantTests : BaseIntegrationTestUseSqliteProgram
    {
        private readonly ITenantService _sut;

        public TenantTests()
        {
            _sut = APIGateway.TenantService;
        }

        [Fact]
        public async Task Should_Get()
        {
            // Arrange
            var first = await _sut.GetFirstAsync();

            // Act
            var result = await _sut.GetAsync(first.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The University of A", result.Name);
            Assert.Equal("universityA", result.Domain);
        }

        [Fact]
        public async Task Should_Get_Tenant2()
        {
            // Arrange
            APIGateway.TenantId = tenant2Id;

            // Act
            var result = await _sut.GetAsyncString(tenant2Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The University of B", result.Name);
            Assert.Equal("universityB", result.Domain);
        }

        [Fact]
        public async Task Should_Update()
        {
            // Act
            var tenant = await _sut.GetAsyncString(tenant1Id);

            // Act
            tenant.EntityNotes = "Some notes";
            var result = await _sut.UpdateAsync(tenant);

            // Assert
            Assert.NotNull(result);
            var assert = await _sut.GetAsync(result.Id);
            Assert.Equal("Some notes", assert.EntityNotes);
        }

    }
}
