using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBase
{
    public interface ICurrentUserInfo
    {
        Guid UserId { get; set; }
        string Account { get; set; }
    }
}
