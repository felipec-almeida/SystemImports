using Main.Application.SystemImports.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Interfaces
{
    public abstract class AService
    {
        protected APIClient _api;
        
        protected AService(APIClient api)
        {
            this._api = api;
        }
    }
}
