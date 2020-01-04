using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserServiceInterface.Dtos
{
    /// <summary>
    /// 用户登录Dto
    /// </summary>
    public class UserLoginDto
    {
        [Required(ErrorMessage = "登录账户不能为空")]
        public string Account { get; set; }
        [Required(ErrorMessage = "登录密码不能为空")]
        public string Password { get; set; }
    }
}
