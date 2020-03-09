using AggregateServiceManager.ServiceRoute;
using ApplicationBase;
using ApplicationBase.Infrastructure.Common;
using BaseServcieInterface;
using GoodsServiceInterface.Dtos;
using InfrastructureBase;
using Oxygen.IServerProxyFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AggregateServiceManager.AggregateService.Goods
{
    public class GetGoodsImageAggreService : AggreServiceBase
    {
        private readonly ICacheService cacheService;
        private readonly string cachekey = "oxygen-goods-image-key";
        public GetGoodsImageAggreService(ICacheService cacheService,IIocContainer container) : base("/api/GoodsService/GetGoodsImage/Query", typeof(GetGoodsImageReq),false, container)
        {
            this.cacheService = cacheService;
        }
        public async override Task<BaseApiResult<object>> Process(object input, IServerProxyFactory serverProxyFactory)
        {
            return await HandleAsync(async () =>
            {
                var value = (GetGoodsImageReq)input;
                var code = cacheService.GetHashCache<string>(cachekey, value.Id);
                return code;
            });
        }
    }
}
