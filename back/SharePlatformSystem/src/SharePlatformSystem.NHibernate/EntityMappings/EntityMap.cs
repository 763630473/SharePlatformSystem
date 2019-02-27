using FluentNHibernate.Mapping;
using SharePlatformSystem.Core.Domain.Entities;
using SharePlatformSystem.NHibernate.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NHibernate.EntityMappings
{
    /// <summary>
    /// 最常用的主键类型“string”的快捷方式“entitymap”。
    /// </summary>
    /// <typeparam name="TEntity">实体映射</typeparam>
    public abstract class EntityMap<TEntity> : EntityMap<TEntity, string> where TEntity : IEntity<string>
    {
        /// <summary>
        /// 构造器.
        /// </summary>
        /// <param name="tableName">表名</param>
        protected EntityMap(string tableName)
            : base(tableName)
        {

        }
    }
    /// <summary>
    /// 此类是将实体映射到数据库表的基类。
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键</typeparam>
    public abstract class EntityMap<TEntity, TPrimaryKey> : ClassMap<TEntity> where TEntity : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="tableName">表名</param>
        protected EntityMap(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException("实体映射的表名(tableName)不能为空");
            }

            Table(tableName);
            Id(x => x.Id);

            //软删除，需要继承软删除接口，自动过滤软删除
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                ApplyFilter<SoftDeleteFilter>();
            }
        }
    }
}