using ApplicationBase;
using Autofac;
using BaseServcieInterface;
using Dapr.Actors;
using Dapr.Actors.Runtime;
using Goods.Domain.Aggregation;
using Goods.Domain.Repository;
using GoodsServiceInterface.Actor;
using GoodsServiceInterface.Actor.Dto;
using GoodsServiceInterface.Dtos;
using Oxygen.DaprActorProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Application.Actor
{
    public class GoodsActor : BaseActor<GoodsEntity>, IGoodsActor
    {
        private readonly ILifetimeScope container;
        public GoodsActor(ActorService actorService,ActorId actorId,ILifetimeScope container) : base(actorService, actorId, container)
        {
            Console.WriteLine($"Actor启动成功：{DateTime.Now}");
            this.container = container;
        }

        public async Task<bool> WithholdingGoodsStock(GoodsActorDto input)
        {
            try
            {
                if (!input.Rollback)
                    instance.SaleGoods(input.StockNumber);
                else
                    instance.RollbackGoods(input.StockNumber);
                if (input.SaveChanges)
                    await base.SaveAll(true);//异步保存
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        public new async Task Delete()
        {
            await base.Delete();
            using (var scope = container.BeginLifetimeScope())
            {
                var goodsRepository = scope.Resolve<IGoodsRepository>();
                goodsRepository.Delete(instance);
                await goodsRepository.SaveAsync();
            }
        }


        protected override async Task SaveInstance()
        {
            //持久化
            try
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var goodsRepository = scope.Resolve<IGoodsRepository>();
                    if (await goodsRepository.AnyAsync(x => x.Id == instance.Id))
                        goodsRepository.Update(instance);
                    else
                        goodsRepository.Add(instance);
                    await goodsRepository.SaveAsync();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"actor持久化失败，异常原因：{e.Message},异常堆栈：{e.StackTrace}");
            }
        }

        public async Task<bool> IncreaseGoods(GoodsActorDto input)
        {
            try
            {
                instance.IncreaseGoods(input.StockNumber);
                if (input.SaveChanges)
                    await base.SaveAll(true);//异步保存
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateBaseInfo(GoodsActorDto input)
        {
            try
            {
                instance.UpdateBaseInfo(input.Name, input.Price);
                if (input.SaveChanges)
                    await base.SaveAll(true);//异步保存
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpOrDownShelf(GoodsActorDto input)
        {
            try
            {
                instance.ChangeShelf(input.IsUpShelf);
                if (input.SaveChanges)
                    await base.SaveAll(true);//异步保存
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
