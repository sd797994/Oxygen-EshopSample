using BaseServcieInterface;
using OrderServiceInterface.Dtos;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderServiceInterface.Query
{

    [AuthSign]
    [RemoteService("OrderService")]
    public interface IGetOrderListByUser
    {
        Task<BaseApiResult<List<QueryOrderDto>>> Query(QueryOrderReq input);
    }
}
