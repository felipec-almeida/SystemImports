namespace Main.Api.SystemImports.Models
{
    public class Cidade
    {
        private int _id;
        private string _nome;
        private string _estadoSigla;

        public int Id { get { return _id; } set { _id = value; } }
        public string Nome { get { return _nome; } set { _nome = value; } }
        public string EstadoSigla { get { return _estadoSigla; } set { _estadoSigla = value; } }

        public Cidade() { }

        public Cidade(int id, string nome, string estadoSigla)
        {
            Id = id;
            Nome = nome;
            EstadoSigla = estadoSigla;
        }
    }
}
