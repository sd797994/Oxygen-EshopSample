using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class GetGoodsInfoByIdReq
    {
        public List<Guid> GoodsIds { get; set; }
    }
}
