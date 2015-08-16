namespace Bazooka.Core
{
    using System;
    using System.Linq;

    /// <summary>
    ///     Static helpers around package namew
    /// </summary>
    public static class PackageHelpers
    {
        /// <summary>
        ///     Extract a package name from a filename
        /// </summary>
        /// <param name="filename">File name </param>
        /// <returns>Package name</returns>
        public static string ExtractPackageName(string filename){
            var newName = filename.Replace(".nupkg","");
            var pieces = newName.Split('.');
            var acc = "";
            int i = 0;
            while (!pieces[i].All(Char.IsDigit)) {
                acc += pieces[i]+ ".";
                i++;
            }

            return acc.Substring(0,acc.Length-1);
        }

        /// <summary>
        ///     Extracts the package version from a file name
        /// </summary>
        /// <param name="filename">File name</param>
        /// <returns>Package version</returns>
        public static string ExtractPackageVersion(string filename)
        {
            var newName = filename.Replace(".nupkg", "");
            var name = ExtractPackageName(filename);
            filename= filename.Replace(name, "").Replace(".nupkg","");
            return filename.Substring(1, filename.Length-1);           
        }
    }
}
