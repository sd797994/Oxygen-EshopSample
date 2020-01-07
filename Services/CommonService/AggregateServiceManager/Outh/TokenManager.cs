using Newtonsoft.Json;
using BaseServcieInterface;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AggregateServiceManager.Auth
{
    public class TokenManager
    {
        private const string IV_64 = "3D63985CC7FE4C95BFF9567E9ED79CEA";
        public static Lazy<ConcurrentDictionary<string, string>> Tokens = new Lazy<ConcurrentDictionary<string, string>>(() => new ConcurrentDictionary<string, string>());
        /// <summary>
        /// 登录创建token
        /// </summary>
        /// <param name="body"></param>
        public static string CreateToken(object body)
        {
            var jwtHeader = JsonConvert.SerializeObject(
                  new { TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body)));
            var encodedString = $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(jwtHeader))}.{base64Payload}";
            var signature = "";
            byte[] keyByte = Encoding.UTF8.GetBytes(IV_64);
            byte[] messageBytes = Encoding.UTF8.GetBytes(encodedString);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                signature = Convert.ToBase64String(hashmessage);
            }
            var token = $"{encodedString}.{signature}";
            if(Tokens.Value.TryGetValue(base64Payload,out string oldtoken))
            {
                return oldtoken;
            }
            else
            {
                Tokens.Value.TryAdd(base64Payload, token);
                return token;
            }
        }
        /// <summary>
        /// 检测token有效性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckToken(string key, out BaseAuthDto tokenObj)
        {
            tokenObj = default;
            if(string.IsNullOrWhiteSpace(key) || key.Split('.').Length != 3)
            {
                return false;
            }
            if (Tokens.Value.TryGetValue(key.Split('.')[1], out string token))
            {
                tokenObj = JsonConvert.DeserializeObject<BaseAuthDto>(Encoding.UTF8.GetString(Convert.FromBase64String(token.Split('.')[1])));
                return true;
            }
            return false;
        }
    }
}