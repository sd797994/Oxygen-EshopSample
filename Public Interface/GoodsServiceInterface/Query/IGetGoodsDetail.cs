using GoodsServiceInterface.Dtos;
using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodsServiceInterface.Query
{
    [RemoteService("GoodsService")]
    public interface IGetGoodsDetail
    {
        Task<BaseApiResult<GoodsQueryDto>> Query(GoodsDetailQueryReq input);
    }
}
