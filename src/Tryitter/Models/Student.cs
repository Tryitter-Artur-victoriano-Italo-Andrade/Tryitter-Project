using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tryitter_Project.Models;

public class Student
{
  [Key]
  public int StudentId { get; set; }

  [Required(ErrorMessage = "Nome é obrigatório")]
  [MaxLength(300, ErrorMessage = "Mensagem deve conter até 300 caracteres")]
  public string UserName { get; set; }

  [MaxLength(300)]
  public string Email { get; set; }
  public string Password { get; set; }

  public string Module { get; set; }

  public string Status { get; set; }

}

