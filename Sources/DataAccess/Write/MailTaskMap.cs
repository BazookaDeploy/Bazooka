
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
namespace DataAccess.Write
{
    public class MailTaskMap : ClassMapping<MailTask>
    {
        public MailTaskMap() {
            Table("MailTasks");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.EnviromentId);
            Property(x => x.ApplicationId);
            Property(x => x.Name, x => x.Length(50));
            Property(x => x.Recipients, x => x.Length(256));
            Property(x => x.Sender, x => { x.NotNullable(true); x.Length(256); });
            Property(X => X.Text, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });
            Property(x => x.Position);
            Property(x => x.Deleted);
        }
    }
}
