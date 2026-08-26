namespace ComicTracker.Application.DTOs.Comics
{
    public class ComicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Writer { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public bool Readed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
