using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccess.Read
{
    public class DeployUnitParameterDto
    {
        [Key]
        public virtual int ParameterId { get; set; }

        public virtual int DeployUnitId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Value { get; set; }
    }
}