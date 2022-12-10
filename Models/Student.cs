namespace TryitterProject.Models;

    public class Student : IStudant
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Module { get; set; }
        public string? Status { get; set; }
        public string? Password { get; set; }
    }

