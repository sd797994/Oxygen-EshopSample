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
    public interface IGetGoodsList
    {
        Task<BaseApiResult<PageQueryDto<GoodsQueryDto>>> Query(GoodsListQueryReq input);
    }
}
