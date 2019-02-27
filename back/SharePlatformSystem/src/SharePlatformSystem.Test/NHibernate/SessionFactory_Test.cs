using NHibernate;
using NUnit.Framework;

namespace SharePlatformSystem.Test.NHibernate
{
    public class SessionFactory_Test : NHibernateTestBase
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionFactory_Test()
        {
            _sessionFactory = Resolve<ISessionFactory>();
        }

        [Test]
        public void Should_OpenSession_Work()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                //nothing...
            }
        }
    }
}