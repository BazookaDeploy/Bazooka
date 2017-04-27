using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Write
{
    public class TaskTemplateVersionMap : ClassMapping<TaskTemplateVersion>
    {
        public TaskTemplateVersionMap()
        {
            Table("TaskTemplateVersions");
            Schema("dbo");
            Id<int>(x => x.Id,
                map =>
                {
                    map.Generator(Generators.Identity);
                    map.Column("Id");
                });

            Property(x => x.Version);
            Property(x => x.Script, x => { x.NotNullable(true); x.Type(NHibernateUtil.StringClob); });

            Bag(x => x.Parameters, map =>
            {
                map.Key(km => km.Column("TaskTemplateVersionId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Inverse(true);
            },
            x => x.OneToMany());
        }
    }
}
