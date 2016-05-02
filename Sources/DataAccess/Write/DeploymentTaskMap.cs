using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
namespace DataAccess.Write
{
    public class DeploymentTaskMap : ClassMapping<DeploymentTask>
    {
        public DeploymentTaskMap()
        {
            Table("DeploymentTasks");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.DeploymentId);
            Property(x => x.DeployTaskId);
            Property(x => x.DeployType);
        }
    }
}