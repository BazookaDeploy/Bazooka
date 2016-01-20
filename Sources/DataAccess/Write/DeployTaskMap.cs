using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class DeployTaskMap : ClassMapping<DeployTask>
    {
        public DeployTaskMap()
        {
            Table("DeployTasks");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Directory, x => x.Length(200));
            Property(x => x.AgentId);
            Property(x => x.Name, x => x.Length(200));
            Property(x => x.EnviromentId, x => x.NotNullable(true));
            Property(x => x.ApplicationId, x => x.NotNullable(true));
            Property(X => X.PackageName);
            Property(x => x.Repository);
            Property(x => x.CurrentlyDeployedVersion);
            Property(x => x.InstallScript,x => { x.Type(NHibernateUtil.StringClob); });
            Property(x => x.UninstallScript, x => { x.Type(NHibernateUtil.StringClob); });
            Property(x => x.ConfigurationFile);
            Property(x => x.ConfigurationTransform, x => { x.Type(NHibernateUtil.StringClob); });
            Property(x => x.Configuration);
            Property(x => x.Position);
            Property(x => x.Deleted);

            Bag(
                x => x.AdditionalParameters,
                map =>
                {
                    map.Key(km => km.Column("DeployTaskId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );
        }
    }
}