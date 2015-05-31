using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Write
{
    public class EnviromentMap : ClassMapping<Enviroment>
    {
        public EnviromentMap()
        {
            Table("Enviroments");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Configuration, x => x.Length(200));
            Property(x => x.Description, x => x.Length(200));
            Property(x => x.ApplicationId, x => x.NotNullable(true));
            Property(x => x.OwnerId, x => x.NotNullable(true));

            Bag(
                x => x.DeployUnits,
                map =>
                {
                    map.Key(km => km.Column("EnviromentId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );
        }
    }
}