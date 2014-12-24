using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mapping
{
    public class DeployLogMap : ClassMapping<DeployLog>
    {
        public DeployLogMap()
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
            Property(x => x.StartDate, x => x.NotNullable(true));
            Property(x => x.DeployUnitId, x => x.NotNullable(true));
        }
    }
}