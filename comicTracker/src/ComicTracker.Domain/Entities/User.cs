using ComicTracker.Domain.Exceptions;

namespace ComicTracker.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        private User() { }

        public static User Create(string firstName, string lastName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName)) 
                throw new DomainException("First Name is required.");

            if (string.IsNullOrWhiteSpace(lastName)) 
                throw new DomainException("Last Name is required.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is required.");

            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("Password is required.");

            return new User
            {
                Email = email.ToLowerInvariant(),
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
