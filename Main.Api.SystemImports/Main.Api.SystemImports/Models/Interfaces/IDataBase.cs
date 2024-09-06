namespace Main.Api.SystemImports.Models.Interfaces
{
    public interface IDataBase
    {
        string Server { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Database { get; set; }

        string CreateConnectionString();
    }
}
