using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryitter_Project.Models;

    public class Student : IStudent
    {
        [Key]
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Module { get; set; }
        public string? Status { get; set; }
        public string? Password { get; set; }
    }

