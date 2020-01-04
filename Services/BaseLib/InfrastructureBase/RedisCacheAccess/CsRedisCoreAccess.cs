using ApplicationBase.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfrastructureBase.RedisCacheAccess
{
    public class CsRedisCoreAccess : ICacheService
    {
        public void InitCacheService(string conn)
        {
            CsRedisFactory.Init(conn);
        }
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            return CsRedisFactory.GetDatabase().Exists(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetCache<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var value = CsRedisFactory.GetDatabase().Get(key);
            if (value == null)
                return default(T);
            return JsonSerializer.Deserialize<T>(value);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCache<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));


            if (Exists(key))
                RemoveCache(key);

            CsRedisFactory.GetDatabase().Set(key, JsonSerializer.Serialize<T>(value));
        }
        /// <summary>
        /// 设置缓存带过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiressAbsoulte"></param>
        public void SetCache<T>(string key, T value, TimeSpan expiressAbsoulte)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));


            if (Exists(key))
                RemoveCache(key);

            int.TryParse(expiressAbsoulte.TotalSeconds.ToString(), out int expiressSecond);
            CsRedisFactory.GetDatabase().Set(key, JsonSerializer.Serialize<T>(value), expiressSecond);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            CsRedisFactory.GetDatabase().Del(key);
        }

        /// <summary>
        /// 获取哈希缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="filed"></param>
        /// <returns></returns>
        public T GetHashCache<T>(string key, string filed) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(filed))
                throw new ArgumentNullException(nameof(filed));


            var value = CsRedisFactory.GetDatabase().HGet(key, filed);
            if (value == null)
                return default(T);

            return JsonSerializer.Deserialize<T>(value);
        }

        /// <summary>
        /// 设置哈希缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetHashCache<T>(string key, string filed, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(filed))
                throw new ArgumentNullException(nameof(filed));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (CsRedisFactory.GetDatabase().HExists(key, filed))
                CsRedisFactory.GetDatabase().HDel(key, filed);
            CsRedisFactory.GetDatabase().HSet(key, filed, JsonSerializer.Serialize<T>(value));
        }
        /// <summary>
        /// 发布消息到对应主题
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filed"></param>
        /// <param name="value"></param>
        public async Task PublishAsync<T>(string channel, T value)
        {
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            await CsRedisFactory.GetDatabase().PublishAsync(channel, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public void Subscribe<T>(string channel, Action<T> func)
        {
            CsRedisFactory.GetDatabase().Subscribe((channel, msg => { func.Invoke(JsonSerializer.Deserialize<T>(msg.Body)); }));
        }
    }
}
