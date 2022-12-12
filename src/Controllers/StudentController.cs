using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

  [HttpGet]
  public ActionResult Get()
  {

    var students = _context.Users.ToList();

    if (students is null)
    {
      return BadRequest("Não foi possível encontrar estudantes, tente novamente");
    }

    return Ok(students);
  }

  [HttpGet("{id}", Name = "GetStudent")]
  public ActionResult Get([FromRoute] string id)
  {
    var student = _context.Users.Find(id);
    return Ok(student);
  }

  [HttpPost]
  public ActionResult Create([FromBody] StudentRequest request)
  {
    if (!request.IsValid)
      return BadRequest(request.Notifications);

    var findStudent = _context.Users.FirstOrDefault(user => user.Email == request.Email);

    if (findStudent is not null)
      return BadRequest("Email existente");

    var student = request.CreateStudent();
    _context.Add(student);
    _context.SaveChanges();

    return CreatedAtRoute("GetStudent", new { student.Id }, student);
  }

  [HttpDelete("{id}")]
  public ActionResult Delete([FromRoute] string id)
  {
    var student = _context.Users.FirstOrDefault(user => user.Id == id);

    if (student is null)
    {
      return NotFound("Estudante não encontrado");
    }

    _context.Users.Remove(student);
    _context.SaveChanges();

    return NoContent();
  }

  [HttpPut("{id}")]
  public ActionResult Put([FromRoute] string id, [FromBody] Student student)
  {
    if (id != student.Id)
    {
      return BadRequest("Id não representa o estudante");
    }

    _context.Entry(student).State = EntityState.Modified;
    _context.SaveChanges();

    return Ok(student);
  }
}

