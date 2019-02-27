using SharePlatformSystem.Core.Auditing.Entities;
using SharePlatformSystem.Core.Domain.Entities;

namespace SharePlatformSystem.Test.DapperAndNHibernate.Entities
{
    public class Person : FullAuditedEntity
    {
        protected Person()
        {
        }

        public Person(string name) : this()
        {
            Name = name;
        }

        public virtual string Name { get; set; }
    }
}
