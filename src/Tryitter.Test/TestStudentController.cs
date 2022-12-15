using Tryitter_Project.Models;
using Tryitter_Project.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Tryitter_Project.Test;


public class TestStudentController : IClassFixture<WebApplicationFactory<Program>>

{
  private readonly WebApplicationFactory<Program> _factory;

  public TestStudentController(WebApplicationFactory<Program> factory) => _factory = factory;

  [Theory(DisplayName = "Teste para rota get /Students")]
  [InlineData("Italo Andrade", "Data science", "Estudante", "123456", "italo@xpi.com")]

  public async Task TestGetAll(string name, string module, string status, string password, string email)
  {
    var student = _factory.CreateClient();
    TokenGenerator instanceToken = new();
    Student instanceStudent = new()
    {
      UserName = name,
      Password = password,
      Status = status,
      Module = module,
      Email = email
    };

    var token = instanceToken.Generate(instanceStudent);

    student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    var res = await student.GetAsync("/Student");
    res.EnsureSuccessStatusCode();
  }

  [Theory(DisplayName = "Teste para a rota post /Students com Status Created")]
  [InlineData("Italo Andrade", "Data science", "Estudante", "123456", "italo123@xpi.com")]
  [InlineData("Arthur Victoriano", "Backend", "Estudante", "123456", "arthur123@xpi.com")]

  public async Task TestPost(string name, string module, string status, string password, string email)
  {
    var student = _factory.CreateClient();
    TokenGenerator instanceToken = new();
    Student instanceStudent = new()
    {
      UserName = name,
      Password = password,
      Status = status,
      Module = module,
      Email = email
    };

    var resPost = await student.PostAsJsonAsync("Student", instanceStudent);
    resPost.EnsureSuccessStatusCode();

    var token = instanceToken.Generate(instanceStudent);

    student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    var res = await student.GetAsync("/Student");
    var content = await res.Content.ReadAsStringAsync();

    res.EnsureSuccessStatusCode();
    content.Contains(instanceStudent.Email).Should().BeTrue();
  }

  [Theory(DisplayName = "Teste para a rota /Students/{id} com Status Ok")]
  [InlineData("Arthur Victoriano", "Backend", "Estudante", "123456", "arthur@xpi.com")]

  public async Task TestGetById(string name, string module, string status, string password, string email)
  {
    var student = _factory.CreateClient();
    TokenGenerator instanceToken = new();
    Student instanceStudent = new()
    {
      UserName = name,
      Password = password,
      Status = status,
      Module = module,
      Email = email
    };

    var token = instanceToken.Generate(instanceStudent);

    student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    var res = await student.GetAsync("/Student/1");
    res.EnsureSuccessStatusCode();
  }

  [Theory(DisplayName = "Teste para a rota delete /Students/{id}")]
  [InlineData("Arthur Victoriano", "Backend", "Estudante", "123456", "arthur@xpi.com")]

  public async Task TestRemoveById(string name, string module, string status, string password, string email)
  {
    var student = _factory.CreateClient();
    TokenGenerator instanceToken = new();
    Student instanceStudent = new()
    {
      UserName = name,
      Password = password,
      Status = status,
      Module = module,
      Email = email
    };

    var token = instanceToken.Generate(instanceStudent);

    student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    await student.DeleteAsync($"/Student/{2}");
    var res = await student.GetAsync($"/Student/{2}");
    res.StatusCode.Should().Be(HttpStatusCode.NotFound);
  }
}
