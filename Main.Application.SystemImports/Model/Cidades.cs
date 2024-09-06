using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class Cidades
    {
        private int _id;
        private string _nome;
        private string _estadoSigla;

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("estadoSigla")]
        public string EstadoSigla { get { return this._estadoSigla; } set { this._estadoSigla = value; } }

        public Cidades() { }

        public Cidades(int id, string nome, string estadoSigla)
        {
            this.Id = id;
            this.Nome = nome;
            this._estadoSigla = estadoSigla;
        }
    }
}
