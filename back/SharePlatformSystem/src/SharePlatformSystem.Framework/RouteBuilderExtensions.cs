﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace SharePlatformSystem.Framework
{
    public static class RouteBuilderExtensions
    {
        public static void ConfigureAll(this List<Action<IRouteBuilder>> routeBuilderActions, IRouteBuilder routes)
        {
            if (routeBuilderActions == null)
            {
                throw new ArgumentNullException(nameof(routeBuilderActions));
            }

            routeBuilderActions.ForEach(action =>
            {
                action(routes);
            });
        }
    }
}