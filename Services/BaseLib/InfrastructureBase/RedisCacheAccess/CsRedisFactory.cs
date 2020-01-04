using System;
using System.Collections.Generic;
using System.Text;
using CSRedis;
using Microsoft.Extensions.Configuration;

namespace InfrastructureBase.RedisCacheAccess
{
    /// <summary>
    /// 缓存客户端工厂
    /// </summary>
    public class CsRedisFactory
    {
        static CSRedisClient client { get; set; }
        public static void Init(string conn)
        {
            client = client ?? new CSRedisClient(conn);
        }
        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static CSRedisClient GetDatabase()
        {
            return client;
        }
    }
}
