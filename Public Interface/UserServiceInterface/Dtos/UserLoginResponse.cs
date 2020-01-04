using System;
using System.Collections.Generic;
using System.Text;

namespace UserServiceInterface.Dtos
{
    /// <summary>
    /// 用户登录返回
    /// </summary>
    public class UserLoginResponse
    {
        public Guid UserId { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; set; }
        public int State { get; set; }
    }
}
