namespace Main.Api.SystemImports.Models
{
    public class Paises
    {
        private int _id;
        private string _nome;

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Nome { get { return this._nome; } set { this._nome = value; } }

        public Paises()
        {
        }

        public Paises(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}
