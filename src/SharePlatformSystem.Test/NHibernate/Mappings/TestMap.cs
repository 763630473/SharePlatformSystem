using SharePlatformSystem.NHibernate.EntityMappings;
using SharePlatformSystem.Test.NHibernate.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Test.NHibernate.Mappings
{
    public class TestMap: EntityMap<TestEntity,string>
    {
        /// <summary>
        ///构造函数.
        /// </summary>
        protected TestMap()
            : base("Test")
        {
            Map(x => x.TestName);
            this.MapFullAudited<TestEntity,string>();
        }
    }
}