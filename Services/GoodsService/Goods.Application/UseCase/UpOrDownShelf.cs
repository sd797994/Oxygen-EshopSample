using ApplicationBase;
using Goods.Domain.Repository;
using GoodsServiceInterface.Dtos;
using GoodsServiceInterface.UseCase;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationException = ApplicationBase.ApplicationException;

namespace Goods.Application.UseCase
{
    public class UpOrDownShelf : BaseUseCase<UpOrDownShelfDto>, IUpOrDownShelf
    {
        private readonly IGoodsRepository goodsRepository;
        public UpOrDownShelf(IGoodsRepository goodsRepository, IIocContainer iocContainer) : base(iocContainer)
        {
            this.goodsRepository = goodsRepository;
        }
        public async Task<BaseApiResult<bool>> Excute(UpOrDownShelfDto input)
        {
            return await HandleAsync(input, async () =>
            {
                //通过仓储获取商品聚合
                var goods = await goodsRepository.GetAsync(input.Id);
                if (goods == null)
                {
                    throw new ApplicationException("没有找到该商品!");
                }
                //商品修改上下架状态
                goods.ChangeShelf(input.IsUpShelf);
                //持久化
                goodsRepository.Update(goods);
                await goodsRepository.SaveAsync();
                return true;
            });
        }
    }
}