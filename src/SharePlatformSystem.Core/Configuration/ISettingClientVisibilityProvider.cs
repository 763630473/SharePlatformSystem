using SharePlatformSystem.Dependency;
using System.Threading.Tasks;

namespace SharePlatformSystem.Core.Configuration
{
    public interface ISettingClientVisibilityProvider
    {
        Task<bool> CheckVisible(IScopedIocResolver scope);
    }
}