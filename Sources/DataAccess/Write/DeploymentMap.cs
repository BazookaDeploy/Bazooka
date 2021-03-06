﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
namespace DataAccess.Write
{
    public class DeploymentMap : ClassMapping<Deployment>
    {
        public DeploymentMap()
        {
            Table("Deployments");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.StartDate, x => x.NotNullable(false));
            Property(x => x.EndDate, x => x.NotNullable(false));
            Property(x => x.EnviromentId, x => x.NotNullable(true));
            Property(x => x.ApplicationId, x => x.NotNullable(true));
            Property(x => x.Status, x => x.NotNullable(true));
            Property(x => x.Version, x => x.NotNullable(true));
            Property(x => x.UserId, x => x.NotNullable(true));
            Property(x => x.Scheduled, x => x.NotNullable(true));

            Bag(
                x => x.Tasks,
                map =>
                {
                    map.Key(km => km.Column("DeploymentId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );
        }
    }
}