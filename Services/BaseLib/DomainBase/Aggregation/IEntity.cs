using System;
using System.Collections.Generic;
using System.Text;

namespace DomainBase
{
    /// <summary>
    /// 实体
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
