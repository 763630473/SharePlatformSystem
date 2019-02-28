using DapperExtensions.Mapper;
using SharePlatformSystem.NHRepository.Entities;

namespace SharePlatformSystem.NHRepository.Mappings.Dapper
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
