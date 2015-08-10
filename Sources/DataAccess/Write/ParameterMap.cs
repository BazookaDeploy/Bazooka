using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class ParameterMap : ClassMapping<Parameter>
    {
        public ParameterMap() {
            Table("DeployTasksParameters");
            Schema("dbo");
            Id<int>(x => x.ParameterId,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Key, x => { x.Length(200); x.Column("Name"); });
            Property(x => x.Value, x => x.Length(200));
            Property(x => x.DeployTaskId, x => x.NotNullable(true));
            Property(x => x.Encrypted);
        }
    }
}