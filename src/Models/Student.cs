using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tryitter_Project.Models;

public class Student : IdentityUser
{
  [Key]
  public string? Module { get; set; }
  public string? Status { get; set; }
}

