using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Event
{
    /// <summary>
    /// 用户创建领域事件
    /// </summary>
    public class UserCreateEvent : CurrentUserEvent, IEvent
    {
        public UserCreateEvent(Guid id)
        {
            this.Id = id;
            UserId = id;
        }
        public Guid Id { get; set; }
    }
}
