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

namespace Tryitter_Project.Test;


public class TestStudentController : IClassFixture<WebApplicationFactory<Program>>

{
  private readonly WebApplicationFactory<Program> _factory;

  public TestStudentController(WebApplicationFactory<Program> factory) => _factory = factory;

  [Trait("Category", "3 - Criar Endpoint Autorização")]
  [Theory(DisplayName = "Teste para PlataformWelcome com Status Ok")]
  // [InlineData("Italo Andrade", "Data science", "Estudante", "123456", "italo@xpi.com")]
  [InlineData("Arthur Victoriano", "Backend", "Estudante", "123456", "arthur@xpi.com")]

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

    // student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // await student.PostAsJsonAsync("Student", instanceStudent);

    var res = await student.GetAsync("/Student/");
    // await res.Content.ReadAsStringAsync();

    res.StatusCode.Should().Be(HttpStatusCode.OK);
    // res.Contains(instanceStudent.Email).Should().BeTrue();
  }

  //   [Trait("Category", "3 - Criar Endpoint Autorização")]
  //   [Theory(DisplayName = "Teste para PlataformWelcome com Status Unauthorized")]
  //   [InlineData("123456789")]
  //   [InlineData("Teste123456")]
  //   [InlineData("INVALIDTOKEN")]

  //   public async Task TestPlataformWelcomeFail(string invalidToken)
  //   {
  //     var Student = _factory.CreateStudent();

  //     Student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);
  //     HttpResponseMessage res = await Student.GetAsync("Student/PlataformWelcome");

  //     res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
  //   }
  // }
  // public class TestStudentController2 : IClassFixture<WebApplicationFactory<Program>>
  // {
  //   private readonly WebApplicationFactory<Program> _factory;
  //   private const string controllerName = "Student";

  //   public TestStudentController2(WebApplicationFactory<Program> factory)
  //   {
  //     _factory = factory;
  //   }

  //   [Trait("Category", "4 - Criar Endpoint com Autorização baseada em Claims")]
  //   [Theory(DisplayName = "Teste para NewPromoAlert com Status Ok")]
  //   [InlineData("Mayara", false, CurrencyEnum.Real)]
  //   [InlineData("Patricia", false, CurrencyEnum.Peso)]
  //   [InlineData("Geni", false, CurrencyEnum.Real)]
  //   [InlineData("Helena", false, CurrencyEnum.Peso)]
  //   public async Task TestNewPromoAlertSuccess(string name, bool isCompany, CurrencyEnum currency)
  //   {
  //     Student Student = new()
  //     {
  //       Name = name,
  //       IsCompany = isCompany,
  //       Currency = currency
  //     };

  //     var token = new TokenGenerator().Generate(Student);
  //     HttpStudent Student = _factory.CreateStudent();

  //     Student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
  //     HttpResponseMessage res = await Student.GetAsync("/Student/NewPromoAlert");

  //     res.StatusCode.Should().Be(HttpStatusCode.OK);
  //   }

  //   [Trait("Category", "4 - Criar Endpoint com Autorização baseada em Claims")]
  //   [Theory(DisplayName = "Teste para NewPromoAlert com Status Forbidden")]
  //   [InlineData("Luiz", false, CurrencyEnum.Euro)]
  //   [InlineData("Ricardo", false, CurrencyEnum.Dolar)]
  //   [InlineData("Trybe", true, CurrencyEnum.Real)]
  //   [InlineData("Paula", true, CurrencyEnum.Dolar)]
  //   public async Task TestNewPromoAlertFail(string name, bool isCompany, CurrencyEnum currency)
  //   {
  //     Student Student = new()
  //     {
  //       Name = name,
  //       IsCompany = isCompany,
  //       Currency = currency
  //     };

  //     var token = new TokenGenerator().Generate(Student);
  //     HttpStudent Student = _factory.CreateStudent();

  //     Student.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
  //     HttpResponseMessage res = await Student.GetAsync("/Student/NewPromoAlert");

  //     res.StatusCode.Should().Be(HttpStatusCode.Forbidden);
  //   }
}
