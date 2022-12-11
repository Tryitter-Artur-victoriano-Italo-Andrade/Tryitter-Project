using Microsoft.AspNetCore.Mvc;
using Tryitter_Project.Context;
using Tryitter_Project.Models;

namespace Tryitter_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
  private TryitterDbContext _context;

  public StudentController(TryitterDbContext context)
  {
    _context = context;
    _context.Database.EnsureCreated();
  }

  [HttpGet("{id}", Name = "GetStudent")]
  public ActionResult Get(string email)
  {
    // if (!request.IsValid)
    //   return BadRequest(request.Notifications);

    var student = _context.Find(Student, email);
    _context.Add(student);
    _context.SaveChanges();

    return CreatedAtRoute("", new { Id = student.Id }, student);
  }

  [HttpPost]
  public ActionResult Create(StudentRequest request)
  {
    if (!request.IsValid)
      return BadRequest(request.Notifications);

    var student = request.CreateStudent();
    _context.Add(student);
    _context.SaveChanges();

    return CreatedAtRoute("GetStudent", new { Id = student.Id }, student);
  }
}
