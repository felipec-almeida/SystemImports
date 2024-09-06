using Main.Application.SystemImports.Interfaces;
using Main.Application.SystemImports.Model;
using Main.Application.SystemImports.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Controller
{
    public class LoginController : IController
    {
        private readonly ServiceLocator _services;

        public IServiceLocator ServiceLocator { get => this._services; }

        public LoginController(ServiceLocator services)
        {
            this._services = services;
        }

        public async void ValidateLogin(string email, string senha, int empresaId)
        {
            var loginService = this._services.GetService<LoginService>();
            loginService.Email = email;
            loginService.Password = BaseConverter.Encrypt(senha, new byte[16], new byte[16]);

            if (empresaId == -1)
            {
                MessageBox.Show("Favor selecionar uma empresa.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            loginService.CompanyId = empresaId;
            var result = await loginService.Login();

            if (result.StartsWith("S^"))
                MessageBox.Show(result.Replace("S^", ""), "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(result.Replace("N^", ""), "Houve um Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 

        public async Task<Dictionary<int, string>> GetEmpresas(Dictionary<int, string> empresas)
        {
            empresas = await this._services.GetService<LoginService>().GetEmpresas();

            if(!empresas.Any())
                MessageBox.Show("Não foram encontradas empresas, favor registrar-se.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return empresas;
        }
    }
}
