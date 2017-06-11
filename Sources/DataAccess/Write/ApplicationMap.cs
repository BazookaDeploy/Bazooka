using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
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
            Property(x => x.ApplicationGroupId);
            Property(x => x.Deleted);
            Property(x => x.Secret);

            Bag(
                x => x.AllowedUsers,
                map =>
                {
                    map.Key(km => km.Column("ApplicationId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );

            Bag(
                x => x.AllowedGroups,
                map =>
                {
                    map.Key(km => km.Column("ApplicationId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );

            Bag(
                x => x.Administrators,
                map =>
                {
                    map.Key(km => km.Column("ApplicationId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );

            Bag(
                x => x.DatabaseTasks,
                map =>
                {
                    map.Key(km => km.Column("ApplicationId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );

            Bag(
                 x => x.DeployTasks,
                 map =>
                 {
                     map.Key(km => km.Column("ApplicationId"));
                     map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                     map.Inverse(true);
                 },
                 x => x.OneToMany()
             );

            Bag(
                 x => x.MailTasks,
                 map =>
                 {
                     map.Key(km => km.Column("ApplicationId"));
                     map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                     map.Inverse(true);
                 },
                 x => x.OneToMany()
             );

            Bag(
                 x => x.LocalScriptTasks,
                 map =>
                 {
                     map.Key(km => km.Column("ApplicationId"));
                     map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                     map.Inverse(true);
                 },
                 x => x.OneToMany()
             );

            Bag(
                 x => x.RemoteScriptTasks,
                 map =>
                 {
                     map.Key(km => km.Column("ApplicationId"));
                     map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                     map.Inverse(true);
                 },
                 x => x.OneToMany()
             );

            Bag(
                 x => x.TemplatedTasks,
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