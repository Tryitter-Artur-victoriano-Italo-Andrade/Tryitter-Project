using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryitter_Project.Models;

    public class Post : IPost
    {
        [Key]
        public int PostId {get; set;}
        public string? Messages { get; set; }
        public string? Image { get; set; }
        
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }

    }
