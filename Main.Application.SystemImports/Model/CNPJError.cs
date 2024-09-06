using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class CNPJError
    {
        private int _status;
        private string _titulo;
        private string _detalhes;
        private List<string> _validacao;

        [JsonPropertyName("status")]
        public int Status { get { return this._status; } set { this._status = value; } }

        [JsonPropertyName("titulo")]
        public string Titulo { get { return this._titulo; } set { this._titulo = value; } }

        [JsonPropertyName("detalhes")]
        public string Detalhes { get { return this._detalhes; } set { this._detalhes = value; } }

        [JsonIgnore]
        public List<string> Validacao { get { return this._validacao; } set { this._validacao = value; } }

        public CNPJError() { }

        public CNPJError(int status, string titulo, string detalhes, List<string> validacao)
        {
            Status = status;
            Titulo = titulo;
            Detalhes = detalhes;
            Validacao = validacao;
        }
    }
}
