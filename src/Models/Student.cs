using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tryitter_Project.Models;

public class Student : IdentityUser
{
  [Key]
  public string Module { get; set; }
  public string Status { get; set; }

  public Student(string username, string email, string password, string module, string status)
  {
    UserName = username;
    Email = email;
    PasswordHash = password;
    Module = module;
    Status = status;
  }
}

