using AggregateServiceManager.ServiceRoute;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BaseServcieInterface;
using Oxygen.CommonTool;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AggregateServiceManager.Auth
{
    /// <summary>
    /// 授权管理器
    /// </summary>
    public class AuthManager
    {
        private static readonly Lazy<ConcurrentDictionary<string, bool>> InstanceDictionary = new Lazy<ConcurrentDictionary<string, bool>>(() => {
            var dictors = new ConcurrentDictionary<string, bool>();
            foreach (var type in RpcInterfaceType.Types.Value)
            {
                var serviceName = (string)typeof(RemoteServiceAttribute).GetProperty("ServerName")
                                                      ?.GetValue(type.GetCustomAttribute(typeof(RemoteServiceAttribute)));
                dictors.TryAdd($"/api/{serviceName}/{type.Name[1..]}/{type.GetMethods().FirstOrDefault().Name}".ToLower(), type.GetCustomAttribute(typeof(AuthSignAttribute)) != null);
            }
            return dictors;
        });
        public static bool CheckAuth(string url, string token, JObject input, Type inputType, out object inputObj)
        {
            inputObj = new object();
            if (InstanceDictionary.Value.TryGetValue(url.ToLower(), out bool needCheck))
            {
                return CheckAuth(needCheck, token, input, inputType, out inputObj);
            }
            return false;
        }
        public static bool CheckAuth(AggreServiceBase routeService, string token, JObject input, Type inputType, out object inputObj)
        {
            return CheckAuth(routeService.NeedAuthCheck, token, input, inputType, out inputObj);
        }
        static bool CheckAuth(bool needCheck, string token, JObject input, Type inputType, out object inputObj)
        {
            inputObj = new object();
            if (needCheck)
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return false;
                }
                if (TokenManager.CheckToken(token, out BaseAuthDto tokenObj))
                {
                    input.Add("UserId", tokenObj.UserId);
                    input.Add("Account", tokenObj.Account);
                    input.Add("Balance", tokenObj.Balance.ToString("0.00"));
                    inputObj = JsonConvert.DeserializeObject(input.ToString(), inputType);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                inputObj = JsonConvert.DeserializeObject(input.ToString(), inputType);
                return true;
            }
        }
    }
}
