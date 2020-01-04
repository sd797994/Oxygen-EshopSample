using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationBase
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="contentObj"></param>
        /// <param name="callbackName"></param>
        void Publish<T>(string name, T contentObj, string callbackName = null);
        /// <summary>
        /// 异步发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="contentObj"></param>
        /// <param name="callbackName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync<T>(string name, T contentObj, string callbackName = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
