using System.Collections.Generic;

namespace SharePlatformSystem.Auditing
{
    /// <summary>
    /// List of selector functions to select classes/interfaces to be audited.
    /// </summary>
    public interface IAuditingSelectorList : IList<NamedTypeSelector>
    {
        /// <summary>
        /// Removes a selector by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool RemoveByName(string name);
    }
}