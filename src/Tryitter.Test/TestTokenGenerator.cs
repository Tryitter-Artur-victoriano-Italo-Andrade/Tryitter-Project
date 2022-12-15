using Tryitter_Project.Models;
using Tryitter_Project.Services;

namespace Tryitter_Project.Test;

public class TestTokenGenerator
{
  [Theory(DisplayName = "Teste para TokenGenerator em que token não é nulo")]
  [InlineData("Italo Andrade", "Data science", "Estudante", "123456", "italo@xpi.com")]
  public void TestTokenGeneratorSuccess(string name, string module, string status, string password, string email)
  {
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
    token.Should().NotBeNullOrEmpty();
  }

  [Theory(DisplayName = "Teste para TokenGenerator em que token JWT possui 3 partes")]
  [InlineData("Arthur Victoriano", "Backend", "Estudante", "123456", "arthur@xpi.com")]
  public void TestTokenGeneratorKeysSuccess(string name, string module, string status, string password, string email)
  {
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

    token.Split('.').Should().HaveCount(3);
  }
}
