using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class LocalScriptTaskMap : ClassMapping<LocalScriptTask>
    {
        public LocalScriptTaskMap()
        {
            Table("LocalScriptTasks");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });
            Property(x => x.EnviromentId);
            Property(x => x.ApplicationId);
            Property(x => x.Name);
            Property(x => x.Script, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });
        }
    }
}
