namespace Jobs
{
    using Bazooka.Core;
    using DataAccess.Read;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    ///     Job for gallety periodic cleaning
    /// </summary>
    public class GalleryCleaningJob
    {
        /// <summary>
        ///     Executes the job
        /// </summary>
        public static void Execute()
        {
            var list = new List<string>();
            var gallery = ConfigurationManager.AppSettings["GalleryAddress"];
            using (var dc = new ReadContext())
            {
                list.AddRange(dc.DeploTasks.Where(x => x.Repository.Trim() == gallery.Trim()).Select(x => x.PackageName.Trim()).Distinct());
            }

            foreach (var package in list)
            {
                CleanPackage(package, gallery);
            }

        }

        /// <summary>
        ///     Clean a specific package removing all nmon necessary versions
        /// </summary>
        /// <param name="package">Package to remove</param>
        /// <param name="gallery">gallery to remove the package from</param>
        public static void CleanPackage(string package, string gallery)
        {
            using (var dc = new ReadContext())
            {
                var allVersion = PackageSearcher.Search(new List<string> { gallery }, package).ToList().Select(x => x.Trim()).ToList();
                var currentlyInstalledVersions = dc.DeploTasks.Where(x => x.PackageName == package && x.CurrentlyDeployedVersion != null).ToList().Select(x => x.CurrentlyDeployedVersion.Trim()).ToList();

                var toDelete = allVersion.Where(x => !currentlyInstalledVersions.Contains(x)).ToList();
                toDelete.RemoveRange(0, 10);

                foreach (var pack in toDelete)
                {
                    RemovePackage(package, pack, gallery);
                }

            }
        }

        /// <summary>
        ///     Tries to remove a package from the gallery
        /// </summary>
        /// <param name="package">Package to remove</param>
        /// <param name="version">Version to remove</param>
        /// <param name="gallery">gallery address</param>
        public static void RemovePackage(string package, string version, string gallery)
        {
            try
            {
                PackageHelpers.Delete(package, version, gallery);
            }
            catch (Exception e)
            {
                //ignore the error we just try to delete
            }
        }
    }
}
