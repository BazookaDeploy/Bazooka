using System;
using System.Linq;

namespace Bazooka.Core
{
    public static class PackageHelpers
    {

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

        public static string ExtractPackageVersion(string filename)
        {
            var newName = filename.Replace(".nupkg", "");
            var name = ExtractPackageName(filename);
            filename= filename.Replace(name, "").Replace(".nupkg","");
            return filename.Substring(1, filename.Length-1);           
        }
    }
}
