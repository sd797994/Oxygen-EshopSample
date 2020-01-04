using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsServiceInterface.Dtos
{
    public class GetGoodsInfoByIdDto
    {
        public Guid GoodsId { get; set; }
        public string GoodsName { get; set; }
        public int Stock { get; set; }
    }
}
