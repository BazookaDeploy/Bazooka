using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class PackageMap : ClassMapping<Package>
    {
        public PackageMap()
        {
            Table("Packages");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Identifier, x => x.NotNullable(true));
            Property(x => x.Version, x => x.NotNullable(true));
        }
    }
}