using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Web.Models
{

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {

        }

        public DbSet<Application> Applications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
        }
    }

    public class ApplicationConfiguration : EntityTypeConfiguration<Application>
    {
        public ApplicationConfiguration()
        {
            ToTable("dbo.Applications");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            HasMany(x => x.Enviroments).WithRequired(x => x.Application).HasForeignKey(x => x.ApplicationId);
        }
    }

}
