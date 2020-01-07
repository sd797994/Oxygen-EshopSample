using ApplicationBase;
using DotNetCore.CAP;
using Hangfire;
using JobRunner.EventHandler.Order.Dto;
using Microsoft.Extensions.Logging;
using OrderServiceInterface.Dtos;
using OrderServiceInterface.UseCase;
using System;
using System.Threading.Tasks;

namespace JobRunner.EventHandler.Order
{
    public class TimeOutCancelOrder : BaseEventHandler<TimeOutCancelOrderDto>, IEventHandler
    {
        public TimeOutCancelOrder(IIocContainer iocContainer) : base(iocContainer)
        {

        }
        [EventHandler("EshopSample.CreateOrderSuccessEvent")]
        public override async Task Handle(TimeOutCancelOrderDto input)
        {
            await HandleAsync(input, async () =>
            {
                BackgroundJob.Schedule<IEventBus>(x => x.Publish("EshopSample.TimeOutCancelOrderEvent", new CancelOrderDto() { OrderId = input.OrderId, UserId = input.UserId, Account = input.Account }, ""), TimeSpan.FromMinutes(30));
            });
        }
    }
}
