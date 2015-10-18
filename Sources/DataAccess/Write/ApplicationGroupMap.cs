using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

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

