using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Main.Application.SystemImports.Interfaces;

namespace Main.Application.SystemImports.Model
{
    public class Usuario : IUsuario
    {
        // Private Interface Members
        private string _email;
        private string _senha;
        private int _empresaId;

        // Private Class Members
        private string _nome;
        private string _sobrenome;
        private ETipoUsuario _tipoUsuario;


        // Public Interface Members
        [JsonPropertyName("email")]
        public string Email { get { return this._email; } set { this._email = value; } }

        [JsonPropertyName("senha")]
        public string Senha { get { return this._senha; } set { this._senha = value; } }

        [JsonPropertyName("empresaId")]
        public int EmpresaId { get { return this._empresaId; } set { this._empresaId = value; } }

        // Public Class Members
        [JsonIgnore]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonIgnore]
        public string Sobrenome { get { return this._sobrenome; } set { this._sobrenome = value; } }

        [JsonIgnore]
        public ETipoUsuario TipoUsuario { get { return this._tipoUsuario; } set { this._tipoUsuario = value; } }

        public Usuario(string email, string senha, int companyId, int tipoUsuario)
        {
            this._email = email;
            this._senha = senha;
            this._empresaId = companyId;
            this.TipoUsuario = (ETipoUsuario)tipoUsuario;
        }
    }
}
