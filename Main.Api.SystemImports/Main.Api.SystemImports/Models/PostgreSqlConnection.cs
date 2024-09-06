using Microsoft.Extensions.Primitives;

namespace Main.Api.SystemImports.Models
{
    public class PostgreSqlConnection : Main.Api.SystemImports.Models.Interfaces.IDataBase
    {
        private string _server;
        private string _username;
        private string _password;
        private int _port;
        private string _database;

        public string Server { get => this._server; set => this._server = value; }
        public string Username { get => this._username; set => this._username = value; }
        public string Password { get => this._password; set => this._password = value; }
        public int Port { get => this._port; set => this._port = value; }
        public string Database { get => this._database; set => this._database = value; }

        public PostgreSqlConnection(string server, string username, string password, int port, string database)
        {
            // Primeiro valida se os dados não estão nulos.
            this.VerifyProperties(server, username, password, port, database);

            this.Server = server;
            this.Username = username;
            this.Password = password;
            this.Port = port;
            this.Database = database;
        }

        public string CreateConnectionString()
        {
            string connectionString = $@"Server={this._server};Port={this._port};User Id={this._username};Password={this._password};Database={this._database};";
            return connectionString;
        }

        /*
         * Responsável por validar os dados durante a nova instância.
         */
        private void VerifyProperties(string server, string username, string password, int port, string database)
        {
            if (string.IsNullOrEmpty(server))
                throw new ArgumentNullException("Server is null or empty.");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("Username is null or empty.");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Password is null or empty.");

            if (string.IsNullOrEmpty(database))
                throw new ArgumentNullException("Database is null or empty.");

            if (port == 0)
                throw new ArgumentNullException("Port is null or empty.");

            return;
        }
    }
}
