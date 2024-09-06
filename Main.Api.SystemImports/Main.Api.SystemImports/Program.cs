using Main.Api.SystemImports.Models;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
 * Nessa etapa, cria uma inst�ncia da Factory e retorna uma connection string, que ir� ser a conex�o do BD.
 */
var dbFactory = new PostgreSqlFactory();
var dbConnection = dbFactory.GetDataBase("localhost", "postgres", "admin", 5432, "systemImportsDB");
var connectionString = dbConnection.CreateConnectionString();
builder.Services.AddTransient<IDbConnection>(x =>
{
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
