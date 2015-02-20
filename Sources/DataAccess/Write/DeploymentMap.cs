using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DataAccess.Write
{
    public class DeploymentMap : ClassMapping<Deployment>
    {
        public DeploymentMap()
        {
            Table("DeployLogs");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Log, x => x.Length(4000));
            Property(x => x.StartDate, x => x.NotNullable(false));
            Property(x => x.EndDate, x => x.NotNullable(false));
            Property(x => x.EnviromentId, x => x.NotNullable(true));
            Property(x => x.Status, x => x.NotNullable(true));
            Property(x => x.Version, x => x.NotNullable(true));
        }
    }
}