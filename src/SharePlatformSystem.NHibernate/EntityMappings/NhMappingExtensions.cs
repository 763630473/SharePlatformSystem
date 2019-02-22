using FluentNHibernate.Mapping;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NHibernate.EntityMappings
{
    /// <summary>
    /// 此类用于使Standart列的映射更加容易。
    /// </summary>
    public static class NhMappingExtensions
    {
        ///// <summary>
        ///// 映射完整审核列（由“ifullAudited”定义）。
        ///// </summary>
        ///// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapFullAudited<TEntity, TPrimaryKey>(this ClassMap<TEntity> mapping)
            where TEntity : IFullAudited<TPrimaryKey>
        {
            mapping.MapAudited<TEntity, TPrimaryKey>();
            mapping.MapDeletionAudited<TEntity, TPrimaryKey>();
        }

        /// <summary>
        /// 映射审核列。见“IAudited”。
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapAudited<TEntity, TPrimaryKey>(this ClassMap<TEntity> mapping) where TEntity : IAudited<TPrimaryKey>
        {
            mapping.MapCreationAudited<TEntity,TPrimaryKey>();
            mapping.MapModificationAudited<TEntity, TPrimaryKey>();
        }

        /// <summary>
        /// 映射创建审核列。“ICreationaudied”
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapCreationAudited<TEntity, TPrimaryKey>(this ClassMap<TEntity> mapping) where TEntity : ICreationAudited<TPrimaryKey>
        {
            mapping.MapCreationTime();
            mapping.Map(x => x.CreatorUserId);
        }

        /// <summary>
        /// 映射CreationTime列。ICreationaudied”
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapCreationTime<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IHasCreationTime
        {
            mapping.Map(x => x.CreationTime);
        }

        /// <summary>
        /// 映射 LastModificationTime 列. 见"IHasModificationTime".
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapLastModificationTime<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IHasModificationTime
        {
            mapping.Map(x => x.LastModificationTime);
        }

        /// <summary>
        /// 映射 modification audit 列. See"IModificationAudited".
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapModificationAudited<TEntity, TPrimaryKey>(this ClassMap<TEntity> mapping) where TEntity : IModificationAudited<TPrimaryKey>
        {
            mapping.MapLastModificationTime();
            mapping.Map(x => x.LastModifierUserId);
        }

        /// <summary>
        /// 映射 deletion列 ("IDeletionAudited").
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapDeletionAudited<TEntity, TPrimaryKey>(this ClassMap<TEntity> mapping) where TEntity : IDeletionAudited<TPrimaryKey>
        {
            mapping.MapIsDeleted();
            mapping.Map(x => x.DeleterUserId);
            mapping.Map(x => x.DeletionTime);
        }

        /// <summary>
        /// 映射 IsDeleted 列 ("ISoftDelete").
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapIsDeleted<TEntity>(this ClassMap<TEntity> mapping) where TEntity : ISoftDelete
        {
            mapping.Map(x => x.IsDeleted);
        }

        /// <summary>
        /// 映射 MapIsActive 列 ("IPassivable").
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static void MapIsActive<TEntity>(this ClassMap<TEntity> mapping) where TEntity : IPassivable
        {
            mapping.Map(x => x.IsActive);
        }
    }
}