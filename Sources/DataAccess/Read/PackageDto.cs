using System;

namespace DataAccess.Read
{
    public class PackageDto 
    {
        public virtual int Id { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Version { get; set; }

        public virtual DateTime UploadDate { get; set; }
    }
}
