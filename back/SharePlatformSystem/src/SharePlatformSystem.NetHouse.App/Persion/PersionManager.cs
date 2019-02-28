using SharePlatformSystem.Core.Domain.Repositories;
using SharePlatformSystem.Dapper.Repositories;
using SharePlatformSystem.NHRepository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePlatformSystem.NetHouse.App.Persion
{
    public class PersionManager: IPersionManager
    {
        private readonly IDapperRepository<Person> _personDapperRepository;
        private readonly IRepository<Person> _personRepository;

        public PersionManager(IDapperRepository<Person> personDapperRepository, IRepository<Person> personRepository)
        {
            _personDapperRepository = personDapperRepository;
            _personRepository = personRepository;          
        }

        public void InsertPersion()
        {
            _personRepository.Insert(new Person("Oguzhan2") { Id = Guid.NewGuid().ToString()});
        }
    }
}
