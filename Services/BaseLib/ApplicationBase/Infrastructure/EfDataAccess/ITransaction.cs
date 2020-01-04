using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBase
{
    public interface ITransaction : IDisposable
    {
        ITransaction BeginTransaction();
        void Commit();
    }
}
