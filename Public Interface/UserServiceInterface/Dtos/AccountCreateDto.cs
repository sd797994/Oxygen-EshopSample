using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserServiceInterface.Dtos
{
    /// <summary>
    /// 用户创建Dto
    /// </summary>
    public class AccountCreateDto
    {
        /// <summary>
        /// 登录账户
        /// </summary>
        [Required(ErrorMessage = "登录账户不能为空")]
        [MinLength(6, ErrorMessage = "登录账户长度不能短于6位")]
        [MaxLength(12, ErrorMessage = "登录账户长度不能超过12位")]
        public string Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "登录密码不能为空")]
        [MinLength(6, ErrorMessage = "登录密码长度不能短于6位")]
        [MaxLength(12, ErrorMessage = "登录密码长度不能超过12位")]
        public string Password { get; set; }
    }
}
