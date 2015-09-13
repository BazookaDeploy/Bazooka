using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class EnviromentMap : ClassMapping<Enviroment>
    {
        public EnviromentMap()
        {
            Table("Enviroments");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name, x => x.Length(200));

            Bag(
                x => x.Agents,
                map =>
                {
                    map.Key(km => km.Column("EnviromentId"));
                    map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    map.Inverse(true);
                },
                x => x.OneToMany()
            );
        }
    }
}