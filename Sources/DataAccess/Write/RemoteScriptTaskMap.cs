namespace DataAccess.Write
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class RemoteScriptTaskMap: ClassMapping<RemoteScriptTask>
    {
        public RemoteScriptTaskMap()
        {
            Table("RemoteScriptTasks");
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
            Property(x => x.Folder);
            Property(x => x.Script, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });
            Property(x => x.AgentId);
        }
    }
}