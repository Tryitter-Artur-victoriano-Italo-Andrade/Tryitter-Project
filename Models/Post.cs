namespace TryitterProject.Models;

    public class Post : IPost
    {
        public int PostId {get; set;}
        public string? Messages { get; set; }
        public string? Image { get; set; }
        public int StudentId { get; set; }

    }
