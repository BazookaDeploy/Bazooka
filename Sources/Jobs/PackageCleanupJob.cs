namespace Jobs
{
    using DataAccess.Read;
    using System.Linq;

    public class PackageCleanupJob
    {
        public static void Execute()
        {
            using (var db = new ReadContext())
            {
                var packages = db.DeploTasks.Select(x => x.PackageName).ToList().Distinct();
            }
        }
    }
}
