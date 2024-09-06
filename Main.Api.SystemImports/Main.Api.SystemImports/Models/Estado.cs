namespace Main.Api.SystemImports.Models
{
    public class Estado
    {
        private int _id;
        private string _nome;
        private string _sigla;
        private int _paisId;

        public int Id { get { return _id; } set { _id = value; } }
        public string Nome { get { return _nome; } set { _nome = value; } }
        public string Sigla { get { return _sigla; } set { _sigla = value; } }
        public int PaisId { get { return _paisId; } set { _paisId = value; } }

        public Estado() { }

        public Estado(int id, string nome, string sigla, int paisId)
        {
            _id = id;
            _nome = nome;
            _sigla = sigla;
            _paisId = paisId;
        }
    }
}
