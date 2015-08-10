using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class LogEntryMap: ClassMapping<LogEntry>
    {
        public LogEntryMap()
        {
            Table("LogEntries");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.DeploymentId, x => x.NotNullable(false));
            Property(x => x.TimeStamp, x => x.NotNullable(false));
            Property(x => x.Error, x => x.NotNullable(true));
            Property(x => x.Text, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });
       }
    }
}
