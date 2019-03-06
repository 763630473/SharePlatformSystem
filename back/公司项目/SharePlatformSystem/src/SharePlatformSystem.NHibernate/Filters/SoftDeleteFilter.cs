using FluentNHibernate.Mapping;
using NHibernate;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.Core.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NHibernate.Filters
{
    /// <summary>
    /// 添加筛选器软删除
    /// </summary>
    public class SoftDeleteFilter : FilterDefinition
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public SoftDeleteFilter()
        {
            WithName(SharePlatformDataFilters.SoftDelete)
                .AddParameter(SharePlatformDataFilters.Parameters.IsDeleted, NHibernateUtil.Boolean)
                .WithCondition($"{nameof(ISoftDelete.IsDeleted)} = :{SharePlatformDataFilters.Parameters.IsDeleted}");
        }
    }
}