using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsServiceInterface.Actor.Dto
{
    public class GoodsActorDto : ActorModel
    {
        [ActorKey]
        public string GoodsId { get; set; }
        public int StockNumber { get; set; }
        public bool Rollback { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsUpShelf { get; set; }
    }
}
