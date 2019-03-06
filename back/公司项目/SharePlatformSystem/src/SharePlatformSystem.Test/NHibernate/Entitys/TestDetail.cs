using FluentNHibernate.Data;
using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.Test.NHibernate.Entitys
{
      [DisableDateTimeNormalization]
    public class TestDetail : Entity, IHasCreationTime
    {
        public virtual TestEntity Test { get; set; }

        public virtual string ItemName { get; set; }

        public virtual int Count { get; set; }

        public virtual decimal TotalPrice { get; set; }

        public virtual DateTime CreationTime { get; set; }
    }
}