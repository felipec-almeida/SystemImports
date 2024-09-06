namespace Main.Api.SystemImports.Models
{
    public class Enderecos
    {
        private int _id;
        private string _rua;
        private int _numero;
        private string _bairro;
        private string _complemento;
        private int _cidadeId;

        public int Id { get { return _id; } set { _id = value; } }
        public string Rua { get { return _rua; } set { _rua = value; } }
        public int Numero { get { return _numero; } set { _numero = value; } }
        public string Bairro { get { return _bairro; } set { _bairro = value; } }
        public string Complemento { get { return _complemento; } set { _complemento = value; } }
        public int CidadeId { get { return _cidadeId; } set { _cidadeId = value; } }

        public Enderecos() { }

        public Enderecos(int id, string rua, int numero, string bairro, string complemento, int cidadeId)
        {
            Id = id;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            CidadeId = cidadeId;
        }
    }
}
