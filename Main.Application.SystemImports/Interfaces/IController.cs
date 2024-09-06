using Main.Application.SystemImports.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Interfaces
{
    public interface IController
    {
        IServiceLocator ServiceLocator { get; }
    }
}
