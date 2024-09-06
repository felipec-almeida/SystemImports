using Main.Application.SystemImports.Interfaces;
using Main.Application.SystemImports.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
            // string decryptedPassword = this.DecryptoLogin();

            // Usuario tmpUser = new Usuario(this._email, this._password, this._companyId);
            var tmpUser = new { email = this._email, senha = this._password, empresaId = this._companyId };

            /*HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlRequest);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/
            //var result = await client.PostAsJsonAsync("/usuario/valida-usuario", tmpUser);

            var result = await Task.Run(() => this._api.SendRequestAsync(ERequestType.POST, urlRequest, tmpUser));

            // Verificar se o resultado é nulo
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseCrudResponse<string>>(result);

            return response.Value;
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
            if (result == null || string.IsNullOrEmpty(result))
            {
                MessageBox.Show("A resposta da API é nula.");
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<BaseResponseList<Empresa>>(result);

            foreach (var item in response.Data)
            {
                listEmpresas.Add(item.Id, item.Nome);
            }

            return listEmpresas;
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
