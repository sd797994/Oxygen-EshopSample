using ApplicationBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public class CurrentUserInfo: ICurrentUserInfo
    {
        public Guid UserId { get; set; }
        public string Account { get; set; }
    }
}
