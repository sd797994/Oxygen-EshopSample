using ApplicationBase;
using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using DotNetCore.CAP.RabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client.Framing;
using Microsoft.Extensions.DependencyInjection;
using DomainBase;

namespace InfrastructureBase
{
    public class EventBus : IEventBus
    {
        public readonly ICapPublisher _publisher;
        private readonly ICurrentUserInfo currentUser;
        public EventBus(ICapPublisher publisher, ICurrentUserInfo currentUser)
        {
            _publisher = publisher;
            this.currentUser = currentUser;
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="contentObj"></param>
        /// <param name="callbackName"></param>
        public void Publish<T>(string name, T contentObj, string callbackName = null)
        {
            if (contentObj is CurrentUserEvent && currentUser != null && currentUser.UserId != Guid.Empty)
            {
                (contentObj as CurrentUserEvent).UserId = currentUser.UserId;
                (contentObj as CurrentUserEvent).Account = currentUser.Account;
            }
            _publisher.Publish(name, contentObj, callbackName);
        }
        /// <summary>
        /// 异步发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="contentObj"></param>
        /// <param name="callbackName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PublishAsync<T>(string name, T contentObj, string callbackName = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (contentObj is CurrentUserEvent && currentUser != null && currentUser.UserId != Guid.Empty)
            {
                (contentObj as CurrentUserEvent).UserId = currentUser.UserId;
                (contentObj as CurrentUserEvent).Account = currentUser.Account;
            }
            await _publisher.PublishAsync(name, contentObj, callbackName, cancellationToken);
        }
    }
}
