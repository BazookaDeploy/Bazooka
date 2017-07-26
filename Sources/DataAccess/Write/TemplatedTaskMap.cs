using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class TemplatedTaskMap : ClassMapping<TemplatedTask>
    {
        public TemplatedTaskMap()
        {
            Table("TemplatedTask");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Name);
            Property(x => x.Position);
            Property(x => x.AgentId);
            Property(x => x.ApplicationId);
            Property(x => x.Deleted);
            Property(x => x.EnviromentId);
            Property(x => x.TaskTemplateVersionId);
            Property(x => x.PackageName);
            Property(x => x.Repository);
            Property(x => x.CurrentlyDeployedVersion);

            Bag(x => x.Prameters, map =>
            {
                map.Key(km => km.Column("TemplatedTaskId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Inverse(true);
            },
            x => x.OneToMany());
        }
    }
}