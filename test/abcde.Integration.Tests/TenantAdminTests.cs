using abcde.Client;
using abcde.Client.Services.Interfaces;
using abcde.Integration.Tests.Base;
using abcde.Test.Data;

namespace abcde.Integration.Tests
{
    public class TenantAdminTests : BaseIntegrationTestUseSqliteProgram
    {
        private readonly ITenantAdminService _sut;

        public TenantAdminTests()
        {
            _sut = APIGateway.TenantAdminService;
        }

        [Fact]
        public async Task Should_Add()
        {
            // Arrange
            var entity = TenantData.Get();

            // Act
            var result = await _sut.AddAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Should_Get()
        {
            // Arrange
            var firstTenant = await _sut.GetFirstAsync();

            // Act
            var result = await _sut.GetAsync(firstTenant.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The University of A", result.Name);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Should_Update()
        {
            // Arrange
            var tenant = await _sut.GetFirstAsync();

            // Act
            tenant.Status = Model.TenantStatus.Suspended;
            var result = await _sut.UpdateAsync(tenant);

            // Assert
            Assert.NotNull(result);

            var assert = await _sut.GetFirstAsync();
            Assert.Equal(Model.TenantStatus.Suspended, assert.Status);
        }

        [Fact]
        public async Task Should_GetInstance_New()
        {
            // Arrange

            // Act
            var result = await _sut.GetInstanceAsync(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(0, result.Id);
            Assert.NotNull(result.TenantId);
        }

        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("The University of A", result.ElementAt(0).Name);
            Assert.Equal("The University of B", result.ElementAt(1).Name);
        }

        [Fact]
        public async Task Should_GetFiltered()
        {
            // Act
            var result = await _sut.GetFilteredAsync(new Model.Filters.TenantAdminFilter() { IsActive = true });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("The University of A", result.ElementAt(0).Name);
            Assert.Equal("The University of B", result.ElementAt(1).Name);
        }

        [Fact]
        public async Task Should_GetFilteredSummary()
        {
            // Act
            var result = await _sut.GetFilteredSummaryAsync(new Model.Filters.TenantAdminFilter() { IsActive = true });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("The University of A", result.ElementAt(0).Name);
            Assert.Equal("The University of B", result.ElementAt(1).Name);
        }

        [Fact]
        public async Task Should_GetAll_Add()
        {
            // Arrange
            var entity = TenantData.Get();
            await _sut.AddAsync(entity);

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task Should_GetAllSummary()
        {
            // Arrange

            // Act
            var result = await _sut.GetAllSummaryAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("a123456789abcdef", result.ElementAt(0).TenantId);
            Assert.Equal("The University of A", result.ElementAt(0).Name);
            Assert.Equal("a987654321", result.ElementAt(1).TenantId);
            Assert.Equal("The University of B", result.ElementAt(1).Name);
        }
    }
}

