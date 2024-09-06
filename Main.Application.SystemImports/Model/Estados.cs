using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class Estados
    {
        private int _id;
        private string _nome;
        private string _sigla;
        private int pais_id;

        [JsonPropertyName("id")]
        public int Id { get { return this._id; } set { this._id = value; } }

        [JsonPropertyName("nome")]
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        [JsonPropertyName("sigla")]
        public string Sigla { get { return this._sigla; } set { this._sigla = value; } }

        [JsonPropertyName("pais_id")]
        public int PaisId { get { return this.pais_id; } set { this.pais_id = value; } }

        public Estados() { }

        public Estados(int id, string nome, string sigla, int pais_id)
        {
            Id = id;
            Nome = nome;
            Sigla = sigla;
            this.pais_id = pais_id;
        }
    }
}
