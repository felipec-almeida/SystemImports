using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Main.Application.SystemImports.Model
{
    public class Endereco
    {
        private int _id;
        private string _rua;
        private int _numero;
        private string _bairro;
        private string _complemento;
        private int _cidadeId;

        [JsonPropertyName("id")]
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [JsonPropertyName("rua")]
        public string Rua
        {
            get { return this._rua; }
            set { this._rua = value; }
        }

        [JsonPropertyName("numero")]
        public int Numero
        {
            get { return this._numero; }
            set { this._numero = value; }
        }

        [JsonPropertyName("bairro")]
        public string Bairro
        {
            get { return this._bairro; }
            set { this._bairro = value; }
        }

        [JsonPropertyName("complemento")]
        public string Complemento
        {
            get { return this._complemento; }
            set { this._complemento = value; }
        }

        [JsonPropertyName("cidadeId")]
        public int CidadeId
        {
            get { return this._cidadeId; }
            set { this._cidadeId = value; }
        }

        public Endereco() { }

        public Endereco(string rua, int numero, string bairro, string complemento, int cidadeId)
        {
            this._rua = rua;
            this._numero = numero;
            this._bairro = bairro;
            this._complemento = complemento;
            this._cidadeId = cidadeId;
        }

        public Endereco(int id, string rua, int numero, string bairro, string complemento, int cidadeId)
        {
            this._id = id;
            this._rua = rua;
            this._numero = numero;
            this._bairro = bairro;
            this._complemento = complemento;
            this._cidadeId = cidadeId;
        }
    }
}
