using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        public Empresa() { }

        public Empresa(string nome, string descricao, string cnpj, int enderecoId, string status)
        {
            Nome = nome;
            Descricao = descricao;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
            Status = status;
        }

        public Empresa(int id, string nome, string descricao, string cnpj, int enderecoId, string status)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
            Status = status;
        }
    }
}
