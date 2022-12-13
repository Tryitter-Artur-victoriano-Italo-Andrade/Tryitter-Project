using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Tryitter_Project.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("UmaSenhaAleatória123"))
  };
});

builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("student", policy =>
  {
    policy.RequireClaim("StudentId");
  });
});

// Adicionar POLICY CLAIMS BASED aqui!
// builder.Services.AddAuthorization(options =>
// {
//   options.AddPolicy("NewPromo", policy =>
// {
//   policy.RequireClaim(
//         "Currency",
//         new List<string>() { CurrencyEnum.Peso.ToString(), CurrencyEnum.Real.ToString() }
//     );
//   policy.RequireClaim("IsCompany", ClientTypeEnum.PessoaFisica.ToString());
// }
// );
// });
/* .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); */
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<TryitterDbContext>(builder.Configuration["Database:SqlServer"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<TryitterDbContext>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
