using BaseServcieInterface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace UserServiceInterface.Dtos
{
    public class AccountLockOrUnLockDto : BaseAuthDto
    {
        [Required(ErrorMessage = "锁定账号ID不能为空")]
        public Guid LockUserId { get; set; }
        [Required(ErrorMessage = "锁定账号状态不能为空")]
        public bool IsLock { get; set; }
    }
}
