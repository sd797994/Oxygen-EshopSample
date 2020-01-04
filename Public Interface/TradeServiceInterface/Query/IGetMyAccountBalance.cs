using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeServiceInterface.Dtos;

namespace TradeServiceInterface.Query
{
    [AuthSign]
    [RemoteService("TradeService")]
    public interface IGetMyAccountBalance
    {
        Task<BaseApiResult<MyAccountResponse>> Query(MyAccountBalanceDto input);
    }
}
