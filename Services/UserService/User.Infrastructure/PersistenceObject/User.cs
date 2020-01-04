using InfrastructureBase;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Aggregation.ValueObject;

namespace User.Infrastructure.PersistenceObject
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : PersistenceObjectBase
    {
        /// <summary>
        /// 登录账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStateEnum State { get; set; }
    }
}
