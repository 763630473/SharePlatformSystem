using SharePlatformSystem.NHibernate.EntityMappings;
using SharePlatformSystem.NHRepository.Entities;

namespace SharePlatformSystem.NHRepository.Mappings.NHibernate
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
