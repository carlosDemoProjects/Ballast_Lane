using ComicTracker.Domain.Exceptions;

namespace ComicTracker.Domain.Entities
{
    public class Comic : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Writer { get; private set; } = string.Empty;
        public string Artist { get; private set; } = string.Empty;
        public string Publisher { get; private set; } = string.Empty;
        public bool Readed { get; private set; }
        public Guid UserId { get; private set; }
        private Comic() { }

        public static Comic Create(string title, string writer, string artist, string publisher, Guid userId, bool readed = false)
        {
            if (userId == Guid.Empty) 
                throw new DomainException("UserId is required.");

            if (string.IsNullOrWhiteSpace(title)) 
                throw new DomainException("Title is required.");

            if (string.IsNullOrWhiteSpace(writer)) 
                throw new DomainException("Writer is required.");

            if (string.IsNullOrWhiteSpace(artist)) 
                throw new DomainException("Artist is required.");

            return new Comic
            {                                
                Title = title,
                Writer = writer,
                Artist = artist,
                Publisher = publisher,                
                UserId = userId,
                Readed = readed
            };
        }

        public void Update(string title, string writer, string artist, string publisher)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required.");

            if (string.IsNullOrWhiteSpace(writer))
                throw new DomainException("Writer is required.");

            if (string.IsNullOrWhiteSpace(artist))
                throw new DomainException("Artist is required.");

            Title = title;
            Artist = artist;
            Writer = writer;    
            Publisher = publisher;            
        }

        public void MarkAsRead() => Readed = true;
    }
}
