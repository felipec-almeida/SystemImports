using System.Text.Json.Serialization;

namespace Main.Application.SystemImports.Model
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

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("descricao")]
        public string Descricao { get { return this._descricao; } set { this._descricao = value; } }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get { return this._cnpj; } set { this._cnpj = value; } }

        [JsonPropertyName("enderecoId")]
        public int EnderecoId { get { return this._enderecoId; } set { this._enderecoId = value; } }

        [JsonPropertyName("status")]
        public string Status { get { return this._status; } set { this._status = value; } }

        [JsonPropertyName("imagemLogo")]
        public string ImagemLogo { get { return this._imagemLogo; } set { this._imagemLogo = value; } }

        public Empresa() { }

        public Empresa(string nome, string descricao, string cnpj, int enderecoId, string status, string imagemLogo)
        {
            Nome = nome;
            Descricao = descricao;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
            Status = status;
            ImagemLogo = imagemLogo;
        }

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
