using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccess.Write
{
    public class MaintenanceTaskMap : ClassMapping<MaintenanceTask>
    {
        public MaintenanceTaskMap()
        {
            Table("MaintenanceTasks");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.AgentId);
            Property(x => x.TemplatedTaskId);
            Property(x => x.StartDate);
            Property(x => x.Status);
            Property(x => x.UserId);
        }
    }
}
