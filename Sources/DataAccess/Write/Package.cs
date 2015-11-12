using System;

namespace DataAccess.Write
{
    public class Package 
    {
        public virtual int Id { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Version { get; set; }

        public virtual DateTime UploadDate { get; set; }
    }
}
