using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class AllowedUserMap: ClassMapping<AllowedUser>
    {
        public AllowedUserMap()
        {
            Table("AllowedUsers");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });


            Property(x => x.EnviromentId, x => x.NotNullable(true));
            Property(x => x.UserId, x => x.NotNullable(true));
        }
    }
}

