using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class DatabaseTaskMap : ClassMapping<DatabaseTask>
    {
        public DatabaseTaskMap()
        {
            Table("DatabaseTasks");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name, x => x.Length(50));
            Property(x => x.EnviromentId, x => x.NotNullable(true));
            Property(x => x.ConnectionString, x => x.Length(256));
            Property(x => x.Package, x => x.Length(256));
            Property(x => x.DatabaseName, x => x.Length(50));
            Property(x => x.Repository, x => x.Length(256));
        }
    }
}