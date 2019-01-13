using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class MaintenanceLogEntryMap : ClassMapping<MaintenanceLogEntry>
    {
        public MaintenanceLogEntryMap()
        {
            Table("MaintenanceLogEntries");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.MaintenanceTaskId, x => x.NotNullable(false));
            Property(x => x.Text, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });
        }
    }
}
