using ApplicationBase;
using BaseServcieInterface;
using Goods.Domain.Repository;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Goods.Application.UseCase
{
    public class DeleteGoods : BaseUseCase<DeleteGoodsReq>, IDeleteGoods
    {
        private readonly IGoodsRepository goodsRepository;
        public DeleteGoods(IGoodsRepository goodsRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
        }
        public async Task<BaseApiResult<bool>> Excute(DeleteGoodsReq input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods = await goodsRepository.GetAsync(input.Id);
                if (goods == null)
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                //持久化
                goodsRepository.Delete(goods);
                await goodsRepository.SaveAsync();
                return true;
            });
        }
    }
}
