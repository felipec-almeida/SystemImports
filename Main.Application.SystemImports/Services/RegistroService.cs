using Main.Application.SystemImports.Interfaces;
using Main.Application.SystemImports.Model;
using System.Text.Json;

namespace Main.Application.SystemImports.Services
{
    public class RegistroService : AService
    {
        public RegistroService(APIClient apiClient) : base(apiClient) { }

        public async Task<Dictionary<string, string>> GetEstados()
        {
            Dictionary<string, string> listEstados = new Dictionary<string, string>();
            string urlRequest = @$"https://localhost:7034/estado/get-page";

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.GET, urlRequest));
            string errorMessage = string.Empty;

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                errorMessage = "A resposta da API é nula.";
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }
            else if (result.Contains("status") || !result.Contains("data"))
            {
                var responseError = JsonSerializer.Deserialize<BaseErrorResponse>(result);
                errorMessage = responseError.Value;
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseResponseList<Estados>>(result);

            foreach (var item in response.Data)
            {
                listEstados.Add(item.Sigla, item.Nome);
            }

            return listEstados;
        }

        public async Task<Dictionary<string, string>> GetCidades()
        {
            Dictionary<string, string> listCidades = new Dictionary<string, string>();

            Dictionary<string, string> listParameters = new Dictionary<string, string>();
            listParameters.Add("pageSize", "1000");

            string urlRequest = @$"https://localhost:7034/cidade/get-page";

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.GET, urlRequest, null, listParameters));
            string errorMessage = string.Empty;

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                errorMessage = "A resposta da API é nula.";
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }
            else if (result.Contains("status") || !result.Contains("data"))
            {
                var responseError = JsonSerializer.Deserialize<BaseErrorResponse>(result);
                errorMessage = responseError.Value;
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseResponseList<Cidades>>(result);

            foreach (var item in response.Data)
            {
                listCidades.Add(item.Nome, item.EstadoSigla);
            }

            return listCidades;
        }

        public async Task<List<Cidades>> GetCidades2()
        {
            List<Cidades> listCidades = new List<Cidades>();

            Dictionary<string, string> listParameters = new Dictionary<string, string>();
            listParameters.Add("pageSize", "1000");

            string urlRequest = @$"https://localhost:7034/cidade/get-page";

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.GET, urlRequest, null, listParameters));

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseResponseList<Cidades>>(result);

            foreach (var item in response.Data)
            {
                listCidades.Add(item);
            }

            return listCidades;
        }

        public async Task<CNPJ> GetCNPJ(string cnpj)
        {
            string urlRequest = $@"https://publica.cnpj.ws/cnpj/{cnpj}";

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.GET, urlRequest));

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            if (result.Contains("status") || result.Contains("400"))
            {
                var responseError = JsonSerializer.Deserialize<CNPJError>(result);

                throw new Exception($"{responseError.Titulo}" + (!string.IsNullOrEmpty(responseError.Detalhes) ? " - " + responseError.Detalhes : string.Empty));
            }

            var response = JsonSerializer.Deserialize<CNPJ>(result);

            return response;
        }

        public async Task<int> InsereEndereco(Endereco endereco)
        {
            string urlRequest = $"https://localhost:7034/enderecos/insert";
            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.POST, urlRequest, endereco, null));

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            if (!result.Contains("id"))
            {
                MessageBox.Show("Houve um erro ao tentar inserir o endereço.", "Erro");
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<Endereco>(result);
            return response.Id;
        }

        public async Task<int> InsereEmpresa(Empresa empresa)
        {
            string urlRequest = $"https://localhost:7034/empresa/insert";
            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.POST, urlRequest, empresa, null));

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            if (!result.Contains("id"))
            {
                MessageBox.Show("Houve um erro ao tentar inserir o endereço.", "Erro");
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<Empresa>(result);
            return response.Id;
        }

        public async Task InsereUsuario(Usuario usuario)
        {
            string urlRequest = $"https://localhost:7034/usuario/insert";
            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.POST, urlRequest, usuario, null));

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            if (!result.Contains("id"))
            {
                MessageBox.Show("Houve um erro ao tentar inserir o usuário.", "Erro");
                throw new Exception();
            }
        }
    }
}
