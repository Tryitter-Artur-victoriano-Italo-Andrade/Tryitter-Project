using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tryitter_Project.Models;

    public class Post : IPost
    {
        [Key]
        public int PostId {get; set;}

        [Required(ErrorMessage = "Mensagem Obrigatória")]
        [MaxLength(300, ErrorMessage ="Mensagem deve conter até 300 caracteres")]
        public string? Messages { get; set; }

        [MaxLength(300)]
        public string? Image { get; set; }
        
        [ForeignKey("StudentId")]
        [Required(ErrorMessage = "Id do estudante Obrigatório")]
        public int StudentId { get; set; }

    }
