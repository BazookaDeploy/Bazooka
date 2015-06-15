namespace Bazooka.Core
{
    using NuGet;

    /// <summary>
    ///     pacckage cache cleaner helpers
    /// </summary>
    public static class PackageCleaner
    {
        /// <summary>
        ///     Cleans the local package cache
        /// </summary>
        public static void Clean()
        {
            MachineCache.Default.Clear();
            OptimizedZipPackage.PurgeCache();
        }
    }
}
