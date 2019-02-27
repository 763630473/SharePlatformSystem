using System;

namespace SharePlatformSystem.Auth.EfRepository.Core
{
    public abstract class Entity
    {
        public string Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
