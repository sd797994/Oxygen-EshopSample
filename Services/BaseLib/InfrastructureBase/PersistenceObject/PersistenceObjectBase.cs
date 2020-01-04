using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public abstract class PersistenceObjectBase
    {
        public PersistenceObjectBase()
        {
            this.Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id;
            this.CreateTime = this.CreateTime == DateTime.MinValue ? DateTime.Now : this.CreateTime;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateUserId { get; set; }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
