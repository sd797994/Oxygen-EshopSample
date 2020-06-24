using BaseServcieInterface;
using Dapr.Actors;
using Goods.Domain.Aggregation;
using GoodsServiceInterface.Actor.Dto;
using GoodsServiceInterface.Dtos;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodsServiceInterface.Actor
{
    [ActorService(false)]
    public interface IGoodsActor : IBaseActor
    {
        Task Add(GoodsEntity instance);
        Task<GoodsEntity> Get();
        Task<bool> WithholdingGoodsStock(GoodsActorDto input);

        Task<bool> IncreaseGoods(GoodsActorDto input);

        Task<bool> UpdateBaseInfo(GoodsActorDto input);

        Task<bool> UpOrDownShelf(GoodsActorDto input);
    }
}
