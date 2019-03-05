using DapperExtensions.Mapper;
using SharePlatformSystem.Test.DapperAndNHibernate.Entities;
using SharePlatformSystem.Test.NHibernate.Entitys;

namespace SharePlatformSystem.Test.DapperAndNHibernate.Mappings.Nhibernate
{
    public class PersonMap : ClassMapper<Person>
    {
        public PersonMap()
        {
            Table("Persons");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}
