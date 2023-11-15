using abcde.Client.Services.Interfaces;
using abcde.Integration.Tests.Base;
using abcde.Model.Exceptions;
using abcde.Model.Filters;

namespace abcde.Integration.Tests
{
    public class NoteTests : BaseIntegrationTestUseSqliteProgram
    {
        private readonly INoteService _sut;

        public NoteTests()
        {
            _sut = APIGateway.NoteService;
        }

        [Fact]
        public async Task Should_Add()
        {
            // Arrange

            // Act
            var result = await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now });

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
            var ex = await Assert.ThrowsAsync<ClientException>(async () => await _sut.AddAsync(new Model.Note() { Date = DateTime.Now }));

            // Assert
            Assert.NotNull(ex);
            Assert.Equal("Note is missing information, please update", ex.Message);
        }

        //[Fact]
        //public async Task Should_Delete()
        //{
        //    // Arrange
        //    var note = await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now });

        //    // Act
        //    await _sut.DeleteAsync(note.Id);

        //    // Assert
        //    var assert = await _sut.GetAsync(note.Id);
        //    Assert.Null(assert);
        //}

        [Fact]
        public async Task Should_GetAll()
        {
            // Arrange
            await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now });

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
            await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now, UserId = Guid.NewGuid() });
            await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now.AddDays(1), UserId = Guid.NewGuid() });

            // Act
            var result = await _sut.GetFilteredAsync(new NoteFilter() { UserId = Guid.NewGuid() });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Should_GetFiltered_By_Date()
        {
            // Arrange
            await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now });
            await _sut.AddAsync(new Model.Note() { NoteText = "test", Date = DateTime.Now.AddDays(1) });

            // Act
            var result = await _sut.GetFilteredAsync(new NoteFilter() { DateString = DateTime.Now.ToString("o") });

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}