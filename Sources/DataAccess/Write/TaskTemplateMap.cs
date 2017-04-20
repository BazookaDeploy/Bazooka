namespace DataAccess.Write
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    public class TaskTemplateMap : ClassMapping<TaskTemplate>
    {
        public TaskTemplateMap()
        {
            Table("TaskTemplates");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });
            Property(x => x.Name);
            Property(x => x.Description);
            Property(x => x.Name);
            Property(x => x.Script, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });

            Bag(x => x.Parameters, map =>
            {
                map.Key(km => km.Column("TaskTemplateId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Inverse(true);
            },
            x => x.OneToMany());
        }
    }
}