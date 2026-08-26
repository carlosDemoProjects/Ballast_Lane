using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Exceptions;
using FluentAssertions;

namespace ComicTracker.Domain.Tests.Entities
{
    public class ComicTests
    {
        private readonly Guid _userId = Guid.NewGuid();

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyTitle_ShouldThrowDomainException(string title)
        {
            var act = () => Comic.Create(title, "Author", "Artist", "Publisher", _userId);
            act.Should().Throw<DomainException>().WithMessage("Title is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyWriter_ShouldThrowDomainException(string writer)
        {
            var act = () => Comic.Create("Batman The Court of Owls", writer, "Artist", "Publisher", _userId);
            act.Should().Throw<DomainException>().WithMessage("Writer is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyArtist_ShouldThrowDomainException(string artist)
        {
            var act = () => Comic.Create("Batman The Court of Owls", "Author", artist, "Publisher", _userId);
            act.Should().Throw<DomainException>().WithMessage("Artist is required.");
        }

        [Fact]
        public void Create_ValidData_ShouldReturnCorrectValues()
        {
            var comic = Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId);

            comic.Title.Should().Be("Batman The Court of Owls");
            comic.Writer.Should().Be("Scott Snyder");
            comic.Artist.Should().Be("Greg Capullo");
            comic.Publisher.Should().Be("DC Comics");            
            comic.Readed.Should().BeFalse();
            comic.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void Create_WithIsReadTrue_ShouldReturnComicMarkedAsRead()
        {
            var comic = Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId, true);

            comic.Readed.Should().BeTrue();
        }

        [Fact]
        public void Update_WithValidData_ShouldUpdateProperties()
        {
            var comic = Comic.Create("Batman The Court of Owl", "Scott Snyder", "Greg Capullo", "DC Comics", _userId);
            comic.Update("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC");
            comic.Title.Should().Be("Batman The Court of Owls");            

            comic.Publisher.Should().Be("DC");
        }

        [Fact]
        public void Update_WithEmptyTitle_ShouldThrowDomainException()
        {
            var comic = Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId);
            var act = () => comic.Update("", "Scott Snyder", "Greg Capullo", "DC Comics");

            act.Should().Throw<DomainException>().WithMessage("Title is required.");
        }        

        [Fact]
        public void MarkAsRead_ShouldSetIsReadToTrue()
        {
            var comic = Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", _userId);
            comic.MarkAsRead();

            comic.Readed.Should().BeTrue();
        }

        [Fact]
        public void Create_WithEmptyUserId_ShouldThrowDomainException()
        {
            Action act = () => Comic.Create("Batman The Court of Owls", "Scott Snyder", "Greg Capullo", "DC Comics", Guid.Empty);
            act.Should().Throw<DomainException>().WithMessage("UserId is required.");
        }
    }
}
