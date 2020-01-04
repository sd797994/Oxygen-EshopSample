using BaseServcieInterface;
using GoodsServiceInterface.Dtos;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodsServiceInterface.UseCase
{
    [AuthSign]
    [RemoteService("GoodsService")]
    public interface IDeleteGoods
    {
        Task<BaseApiResult<bool>> Excute(DeleteGoodsReq input);
    }
}
