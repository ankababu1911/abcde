using abcde.Client.Services.Interfaces;
using abcde.Client;
using abcde.Integration.Tests.Base;
using abcde.Model.Exceptions;
using abcde.Model.Filters;

namespace abcde.Integration.Tests
{
    public class WorkItemTests : BaseIntegrationTestUseSqliteProgram
    {
        private readonly IWorkItemService _sut;

        public WorkItemTests()
        {
            _sut = APIGateway.WorkItemService;
        }

        [Fact]
        public async Task Should_Add()
        {
            // Arrange
            // Act

            var result = await _sut.AddAsync(new Model.WorkItem() { Title = "Goal1", ParentId = null, StartDateTime = DateTime.Now });

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Id.ToString());
            Assert.NotNull(result.TenantId.ToString());
        }

        [Fact]
        public async Task Should_Not_Add_Fail_Validation()
        {
            // Arrange

            // Act
            var ex = await Assert.ThrowsAsync<ClientException>(async () => await _sut.AddAsync(new Model.WorkItem() { StartDateTime = DateTime.Now }));

            // Assert
            Assert.NotNull(ex);
            Assert.Equal("Workitem is missing information, please update", ex.Message);
        }

        [Fact]
        public async Task Should_GetAll()
        {
            // Arrange
            await _sut.AddAsync(new Model.WorkItem() { Title = "Goal 1", StartDateTime = DateTime.Now });

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
            await _sut.AddAsync(new Model.WorkItem() { Title = "Goal 1", StartDateTime = DateTime.Now, UserId = "abcde" });
            await _sut.AddAsync(new Model.WorkItem() { Title = "Goal 2", StartDateTime = DateTime.Now.AddDays(1), UserId = "abcde" });

            // Act
            var result = await _sut.GetFilteredAsync(new WorkItemFilter() { UserId = "abcde" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Should_UpdateIsPinned()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            await _sut.AddAsync(new Model.WorkItem() { Id = guid, Title = "Goal 1", StartDateTime = DateTime.Now, UserId = "abcde" });

            // Act
            var result = await _sut.UpdateIsPinned(guid, true);

            // Assert
            Assert.True(result);
        }
    }
}