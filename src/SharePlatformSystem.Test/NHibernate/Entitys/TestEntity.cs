using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Test.NHibernate.Entitys
{
    public class TestEntity: FullAuditedEntity<string>
    {
        public virtual string TestName { get; set; }
    }
}
