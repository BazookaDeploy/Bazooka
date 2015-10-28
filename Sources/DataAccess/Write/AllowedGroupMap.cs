using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class AllowedGroupMap : ClassMapping<AllowedGroup>
    {
        public AllowedGroupMap()
        {
            Table("AllowedGroups");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });


            Property(x => x.EnviromentId, x => x.NotNullable(true));
            Property(x => x.ApplicationId, x => x.NotNullable(true));
            Property(x => x.GroupId, x => x.NotNullable(true));
        }
    }
}
