using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Test.NHibernate.Entitys
{
    public class TestEntity: FullAuditedEntity<string>, IHasCreationTime
    {
        public virtual string TestName { get; set; }
        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime? ModificationDate { get; set; }
        public virtual ICollection<TestDetail> Items { get; set; }

    }
}
