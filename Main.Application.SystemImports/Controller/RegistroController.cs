using Main.Application.SystemImports.Interfaces;
using Main.Application.SystemImports.Model;
using Main.Application.SystemImports.Services;

namespace Main.Application.SystemImports.Controller
{
    public class RegistroController : IController
    {
        private readonly ServiceLocator _services;

        public IServiceLocator ServiceLocator { get => this._services; }

        public RegistroController(ServiceLocator services)
        {
            this._services = services;
        }

        public async Task<Dictionary<string, string>> GetEstados(Dictionary<string, string> estados)
        {
            estados = await this._services.GetService<RegistroService>().GetEstados();

            if (!estados.Any())
                MessageBox.Show("Sem estados registrados, verificar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return estados;
        }

        public async Task<Dictionary<string, string>> GetCidades(Dictionary<string, string> cidades, string estadoSigla)
        {
            cidades = await this._services.GetService<RegistroService>().GetCidades();

            cidades = cidades.Where(x => x.Value.Equals(estadoSigla)).ToDictionary();

            if (!cidades.Any())
                MessageBox.Show("Sem cidades registradas, verificar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return cidades;
        }

        public string ValidaCNPJ(string cnpj)
        {
            string clearCnpj = cnpj;

            if (!string.IsNullOrEmpty(clearCnpj))
            {
                if (clearCnpj.Contains('/') || clearCnpj.Contains('.') || clearCnpj.Contains(" ") || clearCnpj.Contains('-'))
                {
                    if (clearCnpj.Contains('.'))
                        clearCnpj = clearCnpj.Replace(".", "");

                    if (clearCnpj.Contains('/'))
                        clearCnpj = clearCnpj.Replace("/", "");

                    if (clearCnpj.Contains('-'))
                        clearCnpj = clearCnpj.Replace("-", "");

                    if (clearCnpj.Contains(" "))
                        clearCnpj = clearCnpj.Replace(" ", "");
                }
            }

            return clearCnpj;
        }

        public async Task<CNPJ> GetCNPJ(string cnpj)
        {
            var cnpjResponse = await this._services.GetService<RegistroService>().GetCNPJ(cnpj);

            return cnpjResponse;
        }

        public bool ValidaDados(string nomeEmpresa, string descricaoEmpresa, string cnpj, string rua, string numero, string complemento, string bairro, string cidade)
        {
            if (string.IsNullOrEmpty(nomeEmpresa))
            {
                MessageBox.Show("Campo 'Nome da Empresa' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(descricaoEmpresa))
            {
                MessageBox.Show("Campo 'Descrição da Empresa' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(cnpj))
            {
                MessageBox.Show("Campo 'CNPJ' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(rua))
            {
                MessageBox.Show("Campo 'Rua' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int tmpNumero = int.TryParse(numero, out tmpNumero) ? int.Parse(numero) : 0;
            if (string.IsNullOrEmpty(numero))
            {
                MessageBox.Show("Campo 'Número' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (tmpNumero == 0)
            {
                MessageBox.Show("Campo 'Número' está com caracteres inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(complemento))
            {
                MessageBox.Show("Campo 'Complemento' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(bairro))
            {
                MessageBox.Show("Campo 'Bairro' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(cidade))
            {
                MessageBox.Show("Campo 'Cidade' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public bool ValidaUsuario(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Campo 'Email' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!(email.Contains("@gmail.com") ||
                       email.Contains("@yahoo.com") ||
                       email.Contains("@hotmail.com") ||
                       email.Contains("@outlook.com") ||
                       email.Contains("@icloud.com") ||
                       email.Contains("@mail.com") ||
                       email.Contains("@aol.com") ||
                       email.Contains("@zoho.com") ||
                       email.Contains("@gmx.com") ||
                       email.Contains("@empresa.com") ||
                       email.Contains("@suaempresa.com.br") ||
                       email.Contains("@seudominio.com") ||
                       email.Contains("@hotmail.com.br") ||
                       email.Contains("@uol.com.br") ||
                       email.Contains("@terra.com.br") ||
                       email.Contains("@ig.com.br") ||
                       email.Contains("@protonmail.com") ||
                       email.Contains("@tutanota.com") ||
                       email.Contains("@yandex.com")))
            {
                MessageBox.Show("Campo 'Email' está incorreto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Campo 'Senha' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (senha.Length < 6)
            {
                MessageBox.Show("Campo 'Senha' deve ter pelo menos 6 caracteres", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public async Task<int> InsereEndereco(string rua, string numero, string bairro, string complemento, string nomeCidade)
        {
            var tmpCidades = await this._services.GetService<RegistroService>().GetCidades2();
            int cidadeId = tmpCidades.First(x => x.Nome.ToUpper().Equals(nomeCidade.ToUpper())).Id;
            Endereco endereco = new Endereco(rua, int.Parse(numero), bairro, complemento, cidadeId);
            int enderecoIdResponse = await this._services.GetService<RegistroService>().InsereEndereco(endereco);
            return enderecoIdResponse;
        }

        public async Task<int> InsereEmpresa(string nomeEmpresa, string descricaoEmpresa, string cnpj, int enderecoId)
        {
            Empresa empresa = new Empresa(nomeEmpresa, descricaoEmpresa, cnpj, enderecoId, "true", string.Empty);
            int empresaIdResponse = await this._services.GetService<RegistroService>().InsereEmpresa(empresa);
            return empresaIdResponse;
        }

        public async Task InsereUsuario(string email, string senha, int empresaId, int tipoUsuario)
        {
            Usuario usuario = new Usuario(email, senha, empresaId, tipoUsuario);
            await this._services.GetService<RegistroService>().InsereUsuario(usuario);
        }
    }
}
