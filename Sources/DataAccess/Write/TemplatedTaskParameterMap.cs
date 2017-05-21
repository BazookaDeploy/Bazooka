using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class TemplatedTaskParameterMap : ClassMapping<TemplatedTaskParameter>
    {
        public TemplatedTaskParameterMap()
        {
            Table("TemplatedTaskParameters");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.TaskTemplateParameterId);
            Property(x => x.Value);

            this.ManyToOne(x => x.TemplatedTask, z => {
                z.Column("TemplatedTaskId");
            });
        }
    }
}