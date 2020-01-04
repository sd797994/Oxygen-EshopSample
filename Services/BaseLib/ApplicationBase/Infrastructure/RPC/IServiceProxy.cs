using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBase
{
    public interface IServiceProxy
    {
        T CreateProxy<T>() where T : class;
    }
}
