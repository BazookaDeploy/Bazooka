namespace DataAccess.Write
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class TaskTemplateParameterMap : ClassMapping<TaskTemplateParameter>
    {
        public TaskTemplateParameterMap()
        {
            Table("TaskTemplateParameters");
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
            this.ManyToOne(x => x.Version, z => {
                z.Column("TaskTemplateVersionId");
            });
            //Property(x => x.Version,z => z.Column("TaskTemplateVersionId"));
        }
    }
}