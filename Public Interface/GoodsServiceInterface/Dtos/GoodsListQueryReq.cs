using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class GoodsListQueryReq : PageOrderQueryReq
    {
        public string Name { get; set; }
        public bool? IsUpshelf { get; set; }
    }
}
