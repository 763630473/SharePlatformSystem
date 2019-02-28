using SharePlatformSystem.Events.Bus;
using System.Collections.Generic;

namespace SharePlatformSystem.Core.Domain.Entities
{
    public interface IAggregateRoot : IAggregateRoot<string>, IEntity<string>
    {

    }

    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>, IGeneratesDomainEvents
    {

    }

    public interface IGeneratesDomainEvents
    {
        ICollection<IEventData> DomainEvents { get; }
    }
}