using Main.Api.SystemImports.Models.Interfaces;

namespace Main.Api.SystemImports.Models
{
    public class PostgreSqlFactory : Main.Api.SystemImports.Models.Interfaces.ADataBaseFactory
    {
        public override IDataBase GetDataBase(string server, string username, string password, int port, string database)
        {
            return new PostgreSqlConnection(server, username, password, port, database);
        }
    }
}
