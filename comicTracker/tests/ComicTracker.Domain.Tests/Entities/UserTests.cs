using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Exceptions;
using FluentAssertions;

namespace ComicTracker.Domain.Tests.Entities
{
    public class UserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyEmail_ShouldThrowDomainException(string email)
        {
            var act = () => User.Create("Carlos", "Pérez", email, "hash");

            act.Should().Throw<DomainException>().WithMessage("Email is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyPassword_ShouldThrowDomainException(string password)
        {
            var act = () => User.Create("Carlos", "Pérez", "carlos@email.com", password);

            act.Should().Throw<DomainException>().WithMessage("Password is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyFirstName_ShouldThrowDomainException(string firstName)
        {
            var act = () => User.Create(firstName, "Pérez", "carlos@email.com", "password");

            act.Should().Throw<DomainException>().WithMessage("First Name is required.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Create_WithEmptyLastName_ShouldThrowDomainException(string lastName)
        {
            var act = () => User.Create("Carlos", lastName, "carlos@email.com", "password");

            act.Should().Throw<DomainException>().WithMessage("Last Name is required.");
        }

        [Fact]
        public void Create_WithValidData_ShouldReturnUserWithCorrectValues()
        {
            var user = User.Create("Carlos", "Pérez", "carlos@email.com", "password");

            user.Email.Should().Be("carlos@email.com");
            user.FirstName.Should().Be("Carlos");
            user.LastName.Should().Be("Pérez");
            user.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void Create_ShouldNormalizeEmailToLowercase()
        {
            var user = User.Create("Carlos", "Pérez", "CARLOS@EMAIL.COM", "hash");

            user.Email.Should().Be("carlos@email.com");
        }        
    }
}
