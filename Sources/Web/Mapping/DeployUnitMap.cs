using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mapping
{
    public class DeployUnitMap : ClassMapping<DeployUnit>
    {
        public DeployUnitMap()
        {
            Table("DeployUnits");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Directory, x => x.Length(200));
            Property(x => x.Machine, x => x.Length(200));
            Property(x => x.Name, x => x.Length(200));
            Property(x => x.EnviromentId, x => x.NotNullable(true));

            Bag(
                x => x.AdditionalParameters,
                map =>
                {
                    map.Key(km => km.Column("DeployUnitId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );

            Bag(
                x => x.Logs,
                map =>
                {
                    map.Key(km => km.Column("DeployUnitId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );
        }
    }
}