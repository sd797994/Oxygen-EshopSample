using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBase
{
    public interface IIocContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}
