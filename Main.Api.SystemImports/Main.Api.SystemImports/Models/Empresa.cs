namespace Main.Api.SystemImports.Models
{
    public class Empresa
    {
        private int _id;
        private string _nome;
        private string _descricao;
        private string _cnpj;
        private int _enderecoId;
        private string _status;
        private string _imagemLogo;

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nome { get { return this._nome; } set { this._nome = value; } }
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }
        public string Cnpj { get { return this._cnpj; } set { this._cnpj = value; } }
        public int EnderecoId { get { return this._enderecoId; } set { this._enderecoId = value; } }
        public string Status { get { return this._status; } set { this._status = value; } }
        public string ImagemLogo { get { return this._imagemLogo; } set { this._imagemLogo = value; } }

        public Empresa() { }

        public Empresa(int id, string nome, string descricao, string cnpj, int enderecoId, string status, string imagemLogo)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
            Status = status;
            ImagemLogo = imagemLogo;
        }
    }
}
