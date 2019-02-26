using System;
using Microsoft.AspNetCore.Http;
using SharePlatformSystem.Domain.Uow;

namespace SharePlatformSystem.Framework.AspNetCore.Uow
{
    public class UnitOfWorkMiddlewareOptions
    {
        public Func<HttpContext, bool> Filter { get; set; } = context => true;

        public Func<HttpContext, UnitOfWorkOptions> OptionsFactory { get; set; } = context => new UnitOfWorkOptions();
    }
}
