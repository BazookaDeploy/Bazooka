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

            Bag(x => x.Versions, map =>
            {
                map.Key(km => km.Column("TaskTemplateId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Inverse(true);
            },
            x => x.OneToMany());
        }
    }
}