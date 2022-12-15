using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tryitter_Project.Context;
using Tryitter_Project.Models;
using Tryitter_Project.Services;


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
  [Authorize]
  public ActionResult Get()
  {


    var students = _context.Student.ToList();

    if (students is null)
    {
      return BadRequest("Não foi possível encontrar estudantes, tente novamente");
    }

    return Ok(students);
  }

  [HttpGet("{id}", Name = "GetStudent")]
  [Authorize(Policy = "student")]
  public ActionResult Get(int id)
  {
    var student = _context.Student.FirstOrDefault(x => x.StudentId == id);

    return Ok(student);
  }

  [HttpPost]
  public ActionResult Create([FromBody] Student student)
  {

    // var findStudent = _context.Student.FirstOrDefault(user => user.Email == student.Email);

    // if (findStudent is not null)
    //   return BadRequest("Email existente");

    _context.Add(student);
    _context.SaveChanges();
    var token = new TokenGenerator().Generate(student);
    return new CreatedAtRouteResult("GetStudent", new { id = student.StudentId }, new { token, student });
  }

  [HttpDelete("{id}")]
  [Authorize(Policy = "student")]
  public ActionResult Delete([FromRoute] int id)
  {
    var student = _context.Student.FirstOrDefault(student => student.StudentId == id);

    if (student is null)
    {
      return NotFound("Estudante não encontrado");
    }

    _context.Student.Remove(student);
    _context.SaveChanges();

    return NoContent();
  }

  [HttpPut("{id}")]
  [Authorize(Policy = "student")]
  public ActionResult Put([FromRoute] int id, [FromBody] Student student)
  {
    if (id != student.StudentId)
    {
      return BadRequest("Id não representa o estudante");
    }

    _context.Entry(student).State = EntityState.Modified;
    _context.SaveChanges();

    return Ok(student);
  }
}

