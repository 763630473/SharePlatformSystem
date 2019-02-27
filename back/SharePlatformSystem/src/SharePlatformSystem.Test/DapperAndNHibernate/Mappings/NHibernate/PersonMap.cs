using SharePlatformSystem.NHibernate.EntityMappings;
using SharePlatformSystem.Test.DapperAndNHibernate.Entities;

namespace SharePlatformSystem.Test.DapperAndNHibernate.Mappings.NHibernate
{
    public class PersonMap : EntityMap<Person>
    {
        public PersonMap() : base("Persons")
        {
            Map(x => x.Name);
            this.MapFullAudited<Person, string>();
        }
    }
}
