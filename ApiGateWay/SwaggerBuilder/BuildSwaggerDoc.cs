using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oxygen.CommonTool;
using Oxygen.CsharpClientAgent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using InfrastructureBase;
using AggregateServiceManager.ServiceRoute;
using BaseServcieInterface;

namespace ApiGateWay.SwaggerBuilder
{
    [Route("myswagger.json")]
    [ApiController]
    public class BuildSwaggerDoc
        : ControllerBase
    {
        List<Type> definitionTypes = new List<Type>();
        public string Index()
        {
            JObject obj = new JObject();
            JObject info = new JObject();
            JObject types = new JObject();
            JObject definitions = new JObject();
            info.Add("version", "1.0");
            info.Add("title", "my api");
            obj.Add("swagger", "2.0");
            obj.Add("info", info);
            RouteManager.GetRouteServices().ForEach(x => {
                var inputparm = x.InputType;
                var returnparm = typeof(BaseApiResult<object>);
                var method = new JObject();
                var body = new JObject();
                var responses = new JObject();
                var responsesbody = new JObject();
                body.Add("tags", JToken.FromObject(new string[] { "AggregateService" })); ;
                body.Add("operationId", x.RoutKey);
                body.Add("consumes", JToken.FromObject(new string[] { "application/json", "application/json-patch+json", "text/json", "application/*+json" }));
                body.Add("produces", JToken.FromObject(new string[] { "text/plain", "application/json", "text/json" }));
                var parms = new JObject
                    {
                        { "name", "input" },
                        { "in", "body" },
                        { "required", false }
                    };
                var schema = new JObject
                    {
                        { "$ref", "#/definitions/" + inputparm.Name }
                    };
                parms.Add("schema", schema);
                var parmstoken = new JObject
                    {
                        { "name", "Token" },
                        { "in", "header" },
                        { "required", false },
                        { "type", "string" }
                    };
                body.Add("parameters", JToken.FromObject(new object[] { parms, parmstoken }));
                responsesbody.Add("description", "Success");
                var responsesbodyschema = new JObject
                    {
                        { "$ref", "#/definitions/" + returnparm.Name }
                    };
                responsesbody.Add("schema", responsesbodyschema);
                responses.Add("200", responsesbody);
                body.Add("responses", responses);
                method.Add("post", body);
                types.TryAdd(x.RoutKey, method);
                if (inputparm.IsClass && IsCustomType(inputparm))
                {
                    definitionTypes.Add(inputparm);//入参
                    definitionTypes.AddRange(GetPropertieType(inputparm));//入参属性
                }
                if (inputparm.BaseType != null)
                    definitionTypes.AddRange(GetBaseType(inputparm.BaseType));//入参基类
                definitionTypes.AddRange(GetGenericType(inputparm.GetGenericArguments()));//入参泛型基类
                if (returnparm.IsClass && IsCustomType(returnparm))
                {
                    definitionTypes.Add(returnparm);//出参
                    definitionTypes.AddRange(GetPropertieType(returnparm));//出参属性
                }
                if (returnparm.BaseType != null)
                    definitionTypes.AddRange(GetBaseType(returnparm.BaseType));//出参基类
                definitionTypes.AddRange(GetGenericType(returnparm.GetGenericArguments()));//出参泛型基类
            });
            foreach (var type in RpcInterfaceType.Types.Value)
            {
                //method
                var methodInfo = type.GetMethods()[0];
                var inputparm = methodInfo.GetParameters().FirstOrDefault();
                var returnparm = methodInfo.ReturnType.GetGenericArguments().FirstOrDefault();
                var method = new JObject();
                var body = new JObject();
                var responses = new JObject();
                var responsesbody = new JObject();
                var serviceName = ((RemoteServiceAttribute)type.GetCustomAttribute(typeof(RemoteServiceAttribute))).ServerName;
                var apiurl = $"/api/{serviceName}/{type.Name[1..]}/{type.GetMethods().FirstOrDefault().Name}".ToLower();
                body.Add("tags", JToken.FromObject(new string[] { serviceName }));
                body.Add("operationId", apiurl);
                body.Add("consumes", JToken.FromObject(new string[] { "application/json", "application/json-patch+json", "text/json", "application/*+json" }));
                body.Add("produces", JToken.FromObject(new string[] { "text/plain", "application/json", "text/json" }));
                var parms = new JObject
                {
                    { "name", inputparm.Name },
                    { "in", "body" },
                    { "required", false }
                };
                var schema = new JObject
                {
                    { "$ref", "#/definitions/" + inputparm.ParameterType.Name }
                };
                parms.Add("schema", schema);
                var parmstoken = new JObject
                {
                    { "name", "Token" },
                    { "in", "header" },
                    { "required", false },
                    { "type", "string" }
                };
                body.Add("parameters", JToken.FromObject(new object[] { parms, parmstoken }));
                responsesbody.Add("description", "Success");
                var responsesbodyschema = new JObject
                {
                    { "$ref", "#/definitions/" + returnparm.Name }
                };
                responsesbody.Add("schema", responsesbodyschema);
                responses.Add("200", responsesbody);
                body.Add("responses", responses);
                method.Add("post", body);
                types.TryAdd(apiurl, method);
                if (inputparm.ParameterType.IsClass && IsCustomType(inputparm.ParameterType))
                {
                    definitionTypes.Add(inputparm.ParameterType);//入参
                    definitionTypes.AddRange(GetPropertieType(inputparm.ParameterType));//入参属性
                }
                if (inputparm.ParameterType.BaseType != null)
                    definitionTypes.AddRange(GetBaseType(inputparm.ParameterType.BaseType));//入参基类
                definitionTypes.AddRange(GetGenericType(inputparm.ParameterType.GetGenericArguments()));//入参泛型基类
                if (returnparm.IsClass && IsCustomType(returnparm))
                {
                    definitionTypes.Add(returnparm);//出参
                    definitionTypes.AddRange(GetPropertieType(returnparm));//出参属性
                }
                if (returnparm.BaseType != null)
                    definitionTypes.AddRange(GetBaseType(returnparm.BaseType));//出参基类
                definitionTypes.AddRange(GetGenericType(returnparm.GetGenericArguments()));//出参泛型基类
            }
            obj.Add("paths", types);
            definitionTypes = definitionTypes.Distinct().Where(x=>x.IsClass && IsCustomType(x)).ToList();
            definitionTypes.ForEach(x =>
            {
                if (!definitions.ContainsKey(x.Name))
                {
                    definitions.Add(x.Name, GetParm(x));
                }
            });
            obj.Add("definitions", definitions);
            return JsonConvert.SerializeObject(obj);
        }
        List<Type> GetBaseType(Type type)
        {
            var types = new List<Type>();
            if (type != null)
            {
                if (type.IsClass && IsCustomType(type))
                {
                    types.Add(type);
                    types.AddRange(GetPropertieType(type));
                }
                if (type.BaseType != null)
                {
                    types.AddRange(GetBaseType(type.BaseType));
                }
            }
            return types;
        }
        List<Type> GetGenericType(Type[] typelist)
        {
            var types = new List<Type>();
            typelist.ToList().ForEach(type =>
            {
                if (type != null)
                {
                    if (type.IsClass && IsCustomType(type))
                    {
                        types.Add(type);
                        types.AddRange(GetPropertieType(type));
                    }
                    if (type.GetGenericArguments().Any())
                    {
                        types.AddRange(GetGenericType(type.GetGenericArguments()));
                    }
                }
            });
            return types;
        }
        List<Type> GetPropertieType(Type? typelist)
        {
            var types = new List<Type>();
            typelist.GetProperties().ToList().ForEach(type =>
            {
                if (type != null && type.PropertyType.IsClass && IsCustomType(type.PropertyType))
                {
                    types.Add(type.PropertyType);
                    types.AddRange(GetPropertieType(type.PropertyType));
                }
            });
            return types;
        }
        
        bool IsCustomType(Type type)
        {
            return (type != typeof(object) && Type.GetTypeCode(type) == TypeCode.Object);
        }
        JObject GetParm(Type inputparm)
        {
            var objtype = new JObject();
            var properties = new JObject();
            objtype.Add("type", "object");
            inputparm.GetProperties().ToList().ForEach(x =>
            {
                properties.Add(x.Name, JToken.FromObject(GetFormat(x.PropertyType)));
            });
            objtype.Add("properties", properties);
            return objtype;
        }

        dynamic GetFormat(Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean":
                    return new { type = "boolean" };
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                    return new { type = "integer", format = "int32" };
                case "System.Int64":
                case "System.UInt64":
                    return new { type = "integer", format = "int64" };
                case "System.Single":
                    return new { type = "number", format = "float" };
                case "System.Double":
                case "System.Decimal":
                    return new { type = "number", format = "double" };
                case "System.DateTime":
                case "System.DateTimeOffset":
                    return new { type = "string", format = "date-time" };
                case "System.Guid":
                    return new { type = "string", format = "uuid", example = Guid.Empty };
                case "System.String":
                    return new { type = "string" };
                case "System.Object":
                    return new { type = "string" };
                case "System.Boolean[]":
                    return new { type = "array", items = new { type = "boolean" } };
                case "System.Byte[]":
                case "System.SByte[]":
                case "System.Int16[]":
                case "System.UInt16[]":
                case "System.Int32[]":
                case "System.UInt32[]":
                    return new { type = "array", items = new { type = "integer", format = "int32" } };
                case "System.Int64[]":
                case "System.UInt64[]":
                    return new { type = "array", items = new { type = "integer", format = "int64" } };
                case "System.Single[]":
                    return new { type = "array", items = new { type = "number", format = "float" } };
                case "System.Double[]":
                case "System.Decimal[]":
                    return new { type = "array", items = new { type = "number", format = "double" } };
                case "System.DateTime[]":
                case "System.DateTimeOffset[]":
                    return new { type = "array", items = new { type = "string", format = "date-time" } };
                case "System.Guid[]":
                    return new { type = "array", items = new { type = "string", format = "uuid", example = Guid.Empty } };
                case "System.String[]":
                    return new { type = "array", items = new { type = "string" } };
                case "System.Object[]":
                    return new { type = "array", items = new { type = "string" } };
                default:
                    return GetFormat2(type);
            }
            dynamic GetFormat2(Type type)
            {
                if (type.IsArray && definitionTypes.Select(x => x.Name).Contains(type.Name.Replace("[]", "")))//数组的情况
                {
                    var schema = new JObject
                    {
                        { "$ref", "#/definitions/" + type.Name.Replace("[]", "") }
                    };
                    return new
                    {
                        type = "array",
                        items = schema
                    };
                }
                else if (type.IsGenericType && definitionTypes.Select(x => x.Name).Contains(type.Name))//泛型的情况
                {
                    var schema = new JObject();
                    if(type.Name != "List`1")
                    {
                        schema.Add("$ref", "#/definitions/" + type.Name);
                    }
                    else
                    {
                        schema.Add("$ref", "#/definitions/" + type.GetGenericArguments().FirstOrDefault().Name);
                    }
                    return new
                    {
                        type = "array",
                        items = schema
                    };
                }
                else if(definitionTypes.Select(x => x.Name).Contains(type.Name))
                {
                    var schema = new JObject
                    {
                        { "$ref", "#/definitions/" + type.Name }
                    };
                    return schema;
                }
                else
                {
                    return new { type = "string" };
                }
            }
        }
    }
}