using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using GoodsServiceInterface.Dtos;
using BaseServcieInterface;
using System.Threading.Tasks;

namespace GoodsServiceInterface.UseCase
{
    [AuthSign]
    [RemoteService("GoodsService")]
    public interface IUpOrDownShelf
    {
        Task<BaseApiResult<bool>> Excute(UpOrDownShelfDto input);
    }
}
