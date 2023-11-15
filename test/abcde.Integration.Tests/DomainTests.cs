using abcde.Client.Services.Interfaces;
using abcde.Integration.Tests.Base;
using abcde.Model;
using abcde.Model.Filters;

namespace abcde.Integration.Tests
{
    public class DomainTests: BaseIntegrationTestUseSqliteProgram
    {
        private readonly IDomainService _sut;

        public DomainTests()
        {
            _sut = APIGateway.DomainService;
        }

        [Fact]
        public async Task Should_Add()
        {
            // Arrange

            // Act
            var result = await _sut.AddAsync(new Model.Domain() { Name = "Test Domain" });

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Id.ToString());
            Assert.Equal("Test Domain", result.Name);
        }

        [Fact]
        public async Task Should_GetAll()
        {
            // Arrange
            await _sut.AddAsync(new Model.Domain() { Name = "Test Domain" });

            // Act
            var result = await _sut.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Should_GetFiltered_By_UserId()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            await _sut.AddAsync(new Model.Domain() { Name = "Test Domain 1", DomainUsers = new List<DomainUser> { new DomainUser { UserID = userId } } });
            await _sut.AddAsync(new Model.Domain() { Name = "Test Domain 2", DomainUsers = new List<DomainUser> { new DomainUser { UserID = userId } } });

            // Act
            var result = await _sut.GetFilteredAsync(new DomainFilter() { UserId = userId.ToString() });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
