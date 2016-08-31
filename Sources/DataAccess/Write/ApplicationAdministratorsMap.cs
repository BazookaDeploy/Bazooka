namespace DataAccess.Write
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class ApplicationAdministratorsMap : ClassMapping<ApplicationAdministrator>
    {
        public ApplicationAdministratorsMap()
        {
            Table("ApplicationAdministrators");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });


            Property(x => x.UserId, x => x.NotNullable(true));
            Property(x => x.ApplicationId, x => x.NotNullable(true));
        }
    }
}
