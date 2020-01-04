using GoodsServiceInterface.Dtos;
using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodsServiceInterface.UseCase
{
    [AuthSign]
    [RemoteService("GoodsService")]
    public interface IUpdateGoodsBaseInfo
    {
        Task<BaseApiResult<bool>> Excute(UpdateGoodsBaseInfoDto input);
    }
}
