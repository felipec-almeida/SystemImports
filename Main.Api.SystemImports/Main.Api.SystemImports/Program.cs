using Main.Api.SystemImports.Models;
using Main.Api.SystemImports.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
 * Nessa etapa, cria uma instância da Factory e retorna uma connection string, que irá ser a conexão do BD.
 */
var dbFactory = new PostgreSqlFactory();
var dbConnection = dbFactory.GetDataBase("localhost", "postgres", "admin", 5432, "systemImportsDB");
var connectionString = dbConnection.CreateConnectionString();
builder.Services.AddTransient<IDbConnection>(x =>
{
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddControllers();

var keySession = builder.Configuration.GetSection("Token").Get<string>();

if (string.IsNullOrEmpty(keySession))
    throw new Exception("'Token' não está configurado, tente novamente");

byte[] keySessionEncoded = Encoding.ASCII.GetBytes(keySession);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keySessionEncoded),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
