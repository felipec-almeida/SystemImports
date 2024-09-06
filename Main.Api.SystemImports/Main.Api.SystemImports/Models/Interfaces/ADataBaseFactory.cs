namespace Main.Api.SystemImports.Models.Interfaces
{
    public abstract class ADataBaseFactory
    {
        public abstract IDataBase GetDataBase(string server, string username, string password, int port, string database);
    }
}
