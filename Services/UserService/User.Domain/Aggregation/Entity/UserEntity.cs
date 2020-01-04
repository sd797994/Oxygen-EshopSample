using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Aggregation.ValueObject;

namespace User.Domain.Aggregation
{
    public class UserEntity : AggregateRoot
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
        /// <summary>
        /// 创建password
        /// </summary>
        /// <param name="password"></param>
        public void CreatePassword(string password)
        {
            this.Password = password;
        }

        public bool CheckLogin(string confirmPassword)
        {
            if (!this.Password.Equals(confirmPassword))
            {
                throw new DomainException("用户名或密码错误!");
            }
            if (this.State == UserStateEnum.Frozen)
            {
                throw new DomainException("用户已被锁定,无法登录!");
            }
            return true;
        }

        public void LockOrUnLock(bool isLock)
        {
            if (isLock == true)
            {
                if (State == UserStateEnum.Frozen)
                {
                    throw new DomainException("当前账号已锁定,无法进行锁定操作!");
                }
                State = UserStateEnum.Frozen;
            }
            else
            {
                if (State == UserStateEnum.Normal)
                {
                    throw new DomainException("当前账号已未锁定,无法进行解锁操作!");
                }
                State = UserStateEnum.Normal;
            }
        }
    }
}
