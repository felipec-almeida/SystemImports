using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Application.SystemImports.Interfaces
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}
