using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class ApplicationGroupMap: ClassMapping<ApplicationGroup>
    {
        public ApplicationGroupMap()
        {
            Table("ApplicationGroups");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });


            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}

