using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class AgentMap : ClassMapping<Agent>
    {
        public AgentMap()
        {
            Table("Agents");
            Schema("dbo");
            Id(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name, x => x.Length(50));
            Property(x => x.Address, x => x.Length(50));
            Property(x => x.EnviromentId);
        }
    }
}
