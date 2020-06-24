using ApplicationBase;
using BaseServcieInterface;
using Goods.Domain.Repository;
using GoodsServiceInterface.Actor;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
using Oxygen.DaprActorProvider;
using Oxygen.IServerProxyFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Goods.Application.UseCase
{
    public class DeleteGoods : BaseUseCase<DeleteGoodsReq>, IDeleteGoods
    {
        private readonly IServerProxyFactory serverProxyFactory;
        public DeleteGoods(IServerProxyFactory serverProxyFactory, IIocContainer iocContainer) : base(iocContainer)
        {
            this.serverProxyFactory = serverProxyFactory;
        }
        public async Task<BaseApiResult<bool>> Excute(DeleteGoodsReq input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods =  serverProxyFactory.CreateProxy<IGoodsActor>(input.Id);
                if (!await goods.Exists())
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                //删除对象
                await goods.Delete();
                return true;
            });
        }
    }
}
