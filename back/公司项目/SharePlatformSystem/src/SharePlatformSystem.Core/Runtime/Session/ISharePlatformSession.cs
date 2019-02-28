﻿using System;

namespace SharePlatformSystem.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface ISharePlatformSession
    {
        /// <summary>
        /// Gets current UserId or null.
        /// It can be null if no user logged in.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets current TenantId or null.
        /// This TenantId should be the TenantId of the <see cref="UserId"/>.
        /// It can be null if given <see cref="UserId"/> is a host user or no user logged in.
        /// </summary>

        /// <summary>
        /// UserId of the impersonator.
        /// This is filled if a user is performing actions behalf of the <see cref="UserId"/>.
        /// </summary>
        string ImpersonatorUserId { get; }


        /// <summary>
        /// Used to change <see cref="TenantId"/> and <see cref="UserId"/> for a limited scope.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDisposable Use(string userId);
    }
}
