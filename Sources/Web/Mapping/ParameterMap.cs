using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mapping
{
    public class ParameterMap : ClassMapping<Parameter>
    {
        public ParameterMap() {
            Table("DeployUnitsParameters");
            Schema("dbo");
            Id<int>(x => x.ParameterId,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name, x => x.Length(200));
            Property(x => x.Value, x => x.Length(200));
            Property(x => x.DeployUnitId, x => x.NotNullable(true));
        }
    }
}