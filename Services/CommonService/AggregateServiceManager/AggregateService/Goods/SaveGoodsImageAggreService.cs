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
    public class SaveGoodsImageAggreService : AggreServiceBase
    {
        private readonly ICacheService cacheService;
        private readonly string cachekey = "oxygen-goods-image-key";
        public SaveGoodsImageAggreService(ICacheService cacheService, IIocContainer container) : base("/api/GoodsService/SaveGoodsImage/Excute", typeof(SaveGoodsImageReq), false, container)
        {
            this.cacheService = cacheService;
        }
        public async override Task<BaseApiResult<object>> Process(object input, IServerProxyFactory serverProxyFactory)
        {
            return await HandleAsync(async () =>
            {
                var value = (SaveGoodsImageReq)input;
                var id = Guid.NewGuid().ToString();
                cacheService.SetHashCache(cachekey, id, value.Base64Code);
                return id;
            });
        }
    }
}
