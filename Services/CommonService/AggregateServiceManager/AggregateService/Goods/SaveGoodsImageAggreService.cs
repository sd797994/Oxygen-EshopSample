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
    public class SaveGoodsImageAggreService : AggreServiceBase
    {
        private readonly ICacheService cacheService;
        private readonly string cachekey = "oxygen-goods-image-key";
        public SaveGoodsImageAggreService() : base("/api/GoodsService/SaveGoodsImage/Excute", typeof(SaveGoodsImageReq))
        {
            this.cacheService = IocContainer.Resolve<ICacheService>();
        }
        public async override Task<BaseApiResult<object>> Process(object input)
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
