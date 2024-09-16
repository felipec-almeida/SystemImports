using Main.Application.SystemImports.Interfaces;
using Main.Application.SystemImports.Model;
using System.Text.Json;

namespace Main.Application.SystemImports.Services
{
    public class LoginService : AService, ILogin
    {
        private string _email;
        private string _password;
        private int _companyId;

        public string Email { get => this._email; set => this._email = value; }
        public string Password { get => this._password; set => this._password = value; }
        public int CompanyId { get => this._companyId; set => this._companyId = value; }

        public LoginService(APIClient apiClient) : base(apiClient)
        { }

        public async Task<string> Login()
        {
            string urlRequest = @$"https://localhost:7034/usuario/valida-usuario";

            var tmpUser = new { email = this._email, senha = this._password, empresaId = this._companyId };
            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.POST, urlRequest, tmpUser));

            // Verificar se o resultado é nulo
            string errorMessage = string.Empty;

            if (result == null || string.IsNullOrEmpty(result))
            {
                errorMessage = "A resposta da API é nula.";
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }
            else if ((result.Contains("statusCode") || !result.Contains("data")) && !result.Contains("\"statusCode\":200"))
            {
                var responseError = JsonSerializer.Deserialize<BaseErrorResponse>(result);
                errorMessage = responseError.Value;
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseAuthenticationResponse<string>>(result);

            this._api.SubmitToken(response.Token);

            return response.Response.Value;
        }

        public string LogOff()
        {
            // To Do: API
            return string.Empty;
        }

        public async Task<Dictionary<int, string>> GetEmpresas()
        {
            Dictionary<int, string> listEmpresas = new Dictionary<int, string>();
            string urlRequest = @$"https://localhost:7034/empresa/get-page";

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.GET, urlRequest));

            // Verificar se o resultado é nulo
            string errorMessage = string.Empty;

            if (result == null || string.IsNullOrEmpty(result))
            {
                errorMessage = "A resposta da API é nula.";
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }
            else if ((result.Contains("statusCode") || !result.Contains("data")) && !result.Contains("\"statusCode\":200"))
            {
                var responseError = JsonSerializer.Deserialize<BaseErrorResponse>(result);
                errorMessage = responseError.Value;
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseResponseList<Empresa>>(result);

            foreach (var item in response.Data)
            {
                listEmpresas.Add(item.Id, item.Nome);
            }

            return listEmpresas;
        }

        public async Task<Empresa> GetEmpresa(int id)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("empresaId", id.ToString());

            string urlRequest = @$"https://localhost:7034/empresa/get-one";

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.GET, urlRequest, null, queryParams));

            // Verificar se o resultado é nulo
            string errorMessage = string.Empty;

            if (result == null || string.IsNullOrEmpty(result))
            {
                errorMessage = "A resposta da API é nula.";
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }
            else if ((result.Contains("statusCode") || !result.Contains("data")) && !result.Contains("\"statusCode\":200"))
            {
                var responseError = JsonSerializer.Deserialize<BaseErrorResponse>(result);
                errorMessage = responseError.Value;
                MessageBox.Show(errorMessage, "Erro - API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseCrudResponse<Empresa>>(result);

            return response.Value;
        }

        public string DecryptoLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(this._password))
                {
                    throw new Exception("Campo 'Senha' estava nula");
                }

                return BaseConverter.Decrypt(this._password, new byte[32], new byte[16]);
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um erro ao tentar descriptografar os dados de login.\n\nErro Original ({ex.Message})\n{ex.InnerException}\n{ex.Source}\n{ex.StackTrace}");
            }
        }
    }
}
