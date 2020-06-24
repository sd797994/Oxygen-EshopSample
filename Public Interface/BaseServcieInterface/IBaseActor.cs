using Dapr.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseServcieInterface
{
    public interface IBaseActor : IActor
    {
        Task<bool> Exists();
        Task Delete();
    }
}