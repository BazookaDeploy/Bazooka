namespace Bazooka.Core
{
    using NuGet;
    using System.IO;

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
            var dir = Path.Combine(Path.GetTempPath(), "nuget");
            Directory.Delete(dir,true);
            var dir2 = Path.Combine(Path.GetTempPath(), "NuGetScratch");
            Directory.Delete(dir2, true);
        }
    }
}
