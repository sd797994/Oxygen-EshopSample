using OrderServiceInterface.Dtos;
using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderServiceInterface.UseCase
{
    [AuthSign]
    [RemoteService("OrderService")]
    public interface IOrderCreate
    {
        Task<BaseApiResult<bool>> Excute(OrderCreateReq input);
    }
}
