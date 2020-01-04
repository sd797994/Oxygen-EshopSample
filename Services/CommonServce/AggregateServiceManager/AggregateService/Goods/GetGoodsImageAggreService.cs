using AggregateServiceManager.ServiceRoute;
using ApplicationBase.Infrastructure.Common;
using BaseServcieInterface;
using GoodsServiceInterface.Dtos;
using InfrastructureBase;
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
        public GetGoodsImageAggreService() : base("/api/GoodsService/GetGoodsImage/Query", typeof(GetGoodsImageReq))
        {
            this.cacheService = IocContainer.Resolve<ICacheService>();
        }
        public async override Task<BaseApiResult<object>> Process(object input)
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
