using NHibernate;

namespace SharePlatformSystem.NHibernate
{
    public interface ISessionProvider
    {
        ISession Session { get; }
    }
}