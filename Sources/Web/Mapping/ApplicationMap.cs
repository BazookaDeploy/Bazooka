using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mapping
{
    public class ApplicationMap : ClassMapping<Application>
    {
        public ApplicationMap()
        {
            Table("Applications");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name, x => x.Length(200));

            Bag(
                x => x.Enviroments,
                map =>
                {
                    map.Key(km => km.Column("ApplicationId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );
        }
    }
}