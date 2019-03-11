namespace SharePlatformSystem.Core.Minifier
{
    /// <summary>
    /// 用于缩小JavaScript代码的接口。
    /// </summary>
    public interface IJavaScriptMinifier
    {
        string Minify(string javaScriptCode);
    }
}
