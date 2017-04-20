namespace DataAccess.Write
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class TaskTemplateParameterMap : ClassMapping<TaskTemplateParameter>
    {
        public TaskTemplateParameterMap()
        {
            Table("TaskTemplateParameterss");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name);
            Property(x => x.Optional);
            Property(x => x.Encrypted);
            Property(x => x.TaskTemplateId);
        }
    }
}