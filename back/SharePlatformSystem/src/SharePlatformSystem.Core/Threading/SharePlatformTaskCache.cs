using System.Threading.Tasks;

namespace SharePlatformSystem.Threading
{
    public static class SharePlatformTaskCache
    {
        public static Task CompletedTask { get; } = Task.FromResult(0);
    }
}
