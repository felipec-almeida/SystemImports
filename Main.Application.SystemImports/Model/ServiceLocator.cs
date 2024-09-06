using Main.Application.SystemImports.Interfaces;
using Main.Application.SystemImports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Model
{
    public class ServiceLocator : IServiceLocator
    {
        private APIClient _apiClient;
        private Dictionary<object, object> _services;

        public ServiceLocator(APIClient apiClient)
        {
            this._apiClient = apiClient;
            this._services = new Dictionary<object, object>();

            // Add the Services Here
            this._services.Add(typeof(LoginService), new LoginService(this._apiClient));
            this._services.Add(typeof(RegistroService), new RegistroService(this._apiClient));
        }

        public T GetService<T>()
        {
            return (T)this._services[typeof(T)];
        }
    }
}
