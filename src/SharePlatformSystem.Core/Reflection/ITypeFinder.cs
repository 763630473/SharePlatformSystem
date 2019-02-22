using System;

namespace SharePlatformSystem.Core
{
    public interface ITypeFinder
    {
        Type[] Find(Func<Type, bool> predicate);

        Type[] FindAll();
    }
}