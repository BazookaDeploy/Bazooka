using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mapping
{
    public class ReadContext : DbContext
    {
        public ReadContext() : base("DataContext") { }

        public DbSet<ApplicationDto> Applications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rd");
            modelBuilder.Configurations.Add(new ApplicationDtoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationDtoConfiguration :EntityTypeConfiguration<ApplicationDto> {
        public ApplicationDtoConfiguration() {
            ToTable("Applications");
        }
    }
}