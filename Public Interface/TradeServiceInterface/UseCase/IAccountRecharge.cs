using BaseServcieInterface;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeServiceInterface.Dtos;

namespace TradeServiceInterface.UseCase
{
    [AuthSign]
    [RemoteService("TradeService")]
    public interface IAccountRecharge
    {
        Task<BaseApiResult<bool>> Excute(AccountRechargeDto input);
    }
}
