using SharePlatformSystem.Applications.Services;
using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Dapper.Repositories;
using SharePlatformSystem.NHRepository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NetHouse.App.Persion
{
    public interface IPersionManager:IApplicationService
    {
        void InsertPersion();
    }
}
