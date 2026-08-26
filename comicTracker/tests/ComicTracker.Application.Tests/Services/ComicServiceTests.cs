using ComicTracker.Application.DTOs.Comics;
using ComicTracker.Application.Interfaces;
using ComicTracker.Application.Services;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ComicTracker.Application.Tests.Services
{
    public class ComicServiceTests
    {
        private readonly Mock<IComicRepository> _repositoryMock;
        private readonly IComicService _sut;
        private readonly Guid _userId = Guid.NewGuid();

        public ComicServiceTests()
        {
            _repositoryMock = new Mock<IComicRepository>();
            _sut = new ComicService(_repositoryMock.Object);
        }
        
        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDtos()
        {
            var comics = new List<Comic>
            {
                Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId),
                Comic.Create("Batman The Killing Joke", "Alan Moore", "Brian Bolland", "DC Comics", _userId)
            };
            _repositoryMock.Setup(r => r.GetAllByUserAsync(_userId)).ReturnsAsync(comics);
            
            var result = await _sut.GetAllAsync(_userId);
            result.Should().HaveCount(2);
            result.First().Title.Should().Be("Batman The Court of Owls");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoComics_ShouldReturnEmptyList()
        {
            _repositoryMock.Setup(r => r.GetAllByUserAsync(_userId)).ReturnsAsync(new List<Comic>());
            
            var result = await _sut.GetAllAsync(_userId);
            result.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GetByIdAsync_WhenExists_ShouldReturnDto()
        {
            var comic = Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId);
            _repositoryMock.Setup(r => r.GetByIdAsync(comic.Id)).ReturnsAsync(comic);

            var result = await _sut.GetByIdAsync(comic.Id, _userId);
            result.Should().NotBeNull();
            result!.Title.Should().Be("Batman The Court of Owls");
            result.Id.Should().Be(comic.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenNotFound_ShouldReturnNull()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Comic?)null);

            var result = await _sut.GetByIdAsync(Guid.NewGuid(), _userId);
            result.Should().BeNull();
        }
        
        [Fact]
        public async Task CreateAsync_ShouldCallRepositoryAndReturnDto()
        {
            var dto = new CreateUpdateComicDto
            {
                Title = "Batman The Court of Owls",
                Writer = "Scott Snyder",
                Artist = "Greg Capullo",
                Publisher = "DC Comics"                
            };
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Comic>())).Returns(Task.CompletedTask);

            var result = await _sut.CreateAsync(dto, _userId);
            result.Should().NotBeNull();
            result.Title.Should().Be("Batman The Court of Owls");
            result.Readed.Should().BeFalse();
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Comic>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithIsReadTrue_ShouldReturnComicMarkedAsRead()
        {
            var dto = new CreateUpdateComicDto
            {
                Title = "Batman The Court of Owls",
                Writer = "Scott Snyder",
                Artist = "Greg Capullo",
                Publisher = "DC Comics",
                Readed = true
            };
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Comic>())).Returns(Task.CompletedTask);

            var result = await _sut.CreateAsync(dto, _userId);
            result.Readed.Should().BeTrue();
        }        

        [Fact]
        public async Task UpdateAsync_WhenExists_ShouldUpdateAndReturnDto()
        {
            var comic = Comic.Create("Batman The Court of Owl", "Scott Snyder", "Greg", "DC", _userId);
            var dto = new CreateUpdateComicDto
            {
                Title = "Batman The Court of Owls",
                Writer = "Scott Snyder",
                Artist = "Greg Capullo",
                Publisher = "DC Comics",
            };
            _repositoryMock.Setup(r => r.GetByIdAsync(comic.Id)).ReturnsAsync(comic);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Comic>())).Returns(Task.CompletedTask);

            var result = await _sut.UpdateAsync(comic.Id, dto, _userId);

            result.Title.Should().Be("Batman The Court of Owls");
            result.Artist.Should().Be("Greg Capullo");
            result.Publisher.Should().Be("DC Comics");
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Comic>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldThrowKeyNotFoundException()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Comic?)null);

            var act = async () => await _sut.UpdateAsync(Guid.NewGuid(), new CreateUpdateComicDto(), _userId);
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }        

        [Fact]
        public async Task DeleteAsync_WhenExists_ShouldCallRepository()
        {
            var comic = Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId);
            _repositoryMock.Setup(r => r.GetByIdAsync(comic.Id)).ReturnsAsync(comic);
            _repositoryMock.Setup(r => r.DeleteAsync(comic.Id)).Returns(Task.CompletedTask);

            await _sut.DeleteAsync(comic.Id, _userId);

            _repositoryMock.Verify(r => r.DeleteAsync(comic.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenNotFound_ShouldThrowKeyNotFoundException()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Comic?)null);

            var act = async () => await _sut.DeleteAsync(Guid.NewGuid(), _userId);
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
