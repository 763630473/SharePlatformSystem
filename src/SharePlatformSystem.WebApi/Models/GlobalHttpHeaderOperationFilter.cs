﻿
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharePlatformSystem.WebApi.Model
{
    public class GlobalHttpHeaderOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

#pragma warning disable CS0618 // 类型或成员已过时
            var actionAttrs = context.ApiDescription.ActionAttributes();
#pragma warning restore CS0618 // 类型或成员已过时

            var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (!isAllowAnonymous)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "X-Token",  
                    In = "header",
                    Type = "string",
                    Description = "当前登录用户登录token",
                    Required = false
                });
            }
        }
    }
}
