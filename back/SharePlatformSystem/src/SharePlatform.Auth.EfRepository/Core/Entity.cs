using SharePlatformSystem.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SharePlatformSystem.Auth.EfRepository.Core
{
    public abstract class Entity:IEntity<string>
    {
        public string Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<string>.Default.Equals(Id, default(string)))
            {
                return true;
            }
            return false;
        }
    }
}
