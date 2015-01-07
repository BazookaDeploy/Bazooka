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

        public DbSet<EnviromentDto> Enviroments { get; set; }

        public DbSet<DeployUnitDto> DeployUnits { get; set; }

        public DbSet<DeployUnitParameterDto> DeployUnitsPrameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rd");
            modelBuilder.Configurations.Add(new ApplicationDtoConfiguration());
            modelBuilder.Configurations.Add(new EnviromentDtoConfiguration());
            modelBuilder.Configurations.Add(new DeployUnitDtoConfiguration());
            modelBuilder.Configurations.Add(new DeployUnitParameterDtoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationDtoConfiguration :EntityTypeConfiguration<ApplicationDto> {
        public ApplicationDtoConfiguration() {
            ToTable("Applications");
        }
    }

    public class EnviromentDtoConfiguration : EntityTypeConfiguration<EnviromentDto>
    {
        public EnviromentDtoConfiguration()
        {
            ToTable("Enviroments");
        }
    }

    public class DeployUnitDtoConfiguration : EntityTypeConfiguration<DeployUnitDto>
    {
        public DeployUnitDtoConfiguration()
        {
            ToTable("DeployUnits");
        }
    }

    public class DeployUnitParameterDtoConfiguration : EntityTypeConfiguration<DeployUnitParameterDto>
    {
        public DeployUnitParameterDtoConfiguration()
        {
            ToTable("DeployUnitsParameters");
        }
    }
}