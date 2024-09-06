namespace Main.Api.SystemImports.Models
{
    public class Usuario
    {
        private int _id;
        private string _email;
        private string _senha;
        private int _empresaId;
        private int _tipoUsuarioId;

        public int Id { get { return _id; } set { _id = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Senha { get { return _senha; } set { _senha = value; } }
        public int EmpresaId { get { return _empresaId; } set { _empresaId = value; } }
        public int TipoUsuarioId { get { return _tipoUsuarioId; } set { _tipoUsuarioId = value; } }

        public Usuario() { }

        public Usuario(int id, string email, string senha, int empresaId, int tipoUsuarioId)
        {
            Id = id;
            Email = email;
            Senha = senha;
            EmpresaId = empresaId;
            TipoUsuarioId = tipoUsuarioId;
        }
    }
}
