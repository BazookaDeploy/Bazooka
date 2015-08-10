using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Read
{
    public class ReadContext : DbContext
    {
        public ReadContext() : base("DataContext") { }

        public DbSet<ApplicationDto> Applications { get; set; }

        public DbSet<EnviromentDto> Enviroments { get; set; }

        public DbSet<TaskDto> Tasks { get; set; }

        public DbSet<DeployTaskDto> DeploTasks { get; set; }
        public DbSet<DeployTaskParameterDto> DeployUnitsPrameters { get; set; }
        public DbSet<MailTaskDto> MailTasks { get; set; }
        public DbSet<LocalScriptTaskDto> LocalScriptTasks { get; set; }
        public DbSet<RemoteScriptTaskDto> RemoteScriptTasks { get; set; }


        public DbSet<DeploymentDto> Deployments { get; set; }

        public DbSet<UserDto> Users { get; set; }
        public DbSet<GroupDto> Groups { get; set; }
        public DbSet<UsersInGroupsDto> UsersInGroups { get; set; }

        public DbSet<AllowedUsersDto> AllowedUsers { get; set; }
        public DbSet<AllowedGroupsDto> AllowedGroups { get; set; }
        public DbSet<DeployerDto> Deployers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rd");
            modelBuilder.Configurations.Add(new ApplicationDtoConfiguration());
            modelBuilder.Configurations.Add(new EnviromentDtoConfiguration());
            modelBuilder.Configurations.Add(new DeployUnitDtoConfiguration());
            modelBuilder.Configurations.Add(new DeployUnitParameterDtoConfiguration());
            modelBuilder.Configurations.Add(new DeploymentDtoConfiguration());
            modelBuilder.Configurations.Add(new UsersInGroupsDtoConfiguration());
            modelBuilder.Configurations.Add(new GroupsDtoConfiguration());
            modelBuilder.Configurations.Add(new UserDtoConfiguration());
            modelBuilder.Configurations.Add(new AllowedUsersDtoConfiguration());
            modelBuilder.Configurations.Add(new AllowedGroupsDtoConfiguration());
            modelBuilder.Configurations.Add(new DeployersDtoConfiguration());
            modelBuilder.Configurations.Add(new LogEntryDtoConfiguration());
            modelBuilder.Configurations.Add(new TasksDtoConfiguration());
            modelBuilder.Configurations.Add(new MailTasksDtoConfiguration());
            modelBuilder.Configurations.Add(new LocalScriptTaskDtoConfiguration());
            modelBuilder.Configurations.Add(new RemoteScriptTaskDtoConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }




    public class DeployersDtoConfiguration : EntityTypeConfiguration<DeployerDto>
    {
        public DeployersDtoConfiguration()
        {
            ToTable("Deployers");
            HasKey(x => new
            {
                x.ApplicationId,
                x.EnviromentId,
                x.UserId
            });
        }
    }

    public class TasksDtoConfiguration : EntityTypeConfiguration<TaskDto>
    {
        public TasksDtoConfiguration()
        {
            ToTable("Tasks");
            HasKey(x => new {x.EnviromentId, x.Id, x.Type });
        }
    }

    public class MailTasksDtoConfiguration : EntityTypeConfiguration<MailTaskDto>
    {
        public MailTasksDtoConfiguration()
        {
            ToTable("MailTasks");
        }
    }

    public class LocalScriptTaskDtoConfiguration : EntityTypeConfiguration<LocalScriptTaskDto>
    {
        public LocalScriptTaskDtoConfiguration()
        {
            ToTable("LocalScriptTasks");
        }
    }

    public class RemoteScriptTaskDtoConfiguration : EntityTypeConfiguration<RemoteScriptTaskDto>
    {
        public RemoteScriptTaskDtoConfiguration()
        {
            ToTable("RemoteScriptTasks");
        }
    }

    public class AllowedGroupsDtoConfiguration : EntityTypeConfiguration<AllowedGroupsDto>
    {
        public AllowedGroupsDtoConfiguration()
        {
            ToTable("AllowedGroups");
        }
    }

    public class AllowedUsersDtoConfiguration : EntityTypeConfiguration<AllowedUsersDto>
    {
        public AllowedUsersDtoConfiguration()
        {
            ToTable("AllowedUsers");
        }
    }

    public class UsersInGroupsDtoConfiguration : EntityTypeConfiguration<UsersInGroupsDto>
    {
        public UsersInGroupsDtoConfiguration()
        {
            ToTable("UsersInGroups");
            HasKey(x => new { x.RoleId, x.UserId });
        }
    }


    public class GroupsDtoConfiguration : EntityTypeConfiguration<GroupDto>
    {
        public GroupsDtoConfiguration()
        {
            ToTable("Groups");
        }
    }

    public class UserDtoConfiguration : EntityTypeConfiguration<UserDto>
    {
        public UserDtoConfiguration()
        {
            ToTable("Users");
        }
    }

    public class LogEntryDtoConfiguration : EntityTypeConfiguration<LogEntryDto>
    {
        public LogEntryDtoConfiguration()
        {
            ToTable("Logs");
        }
    }

    public class ApplicationDtoConfiguration : EntityTypeConfiguration<ApplicationDto>
    {
        public ApplicationDtoConfiguration()
        {
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

    public class DeployUnitDtoConfiguration : EntityTypeConfiguration<DeployTaskDto>
    {
        public DeployUnitDtoConfiguration()
        {
            ToTable("DeployTasks");
            HasMany<DeployTaskParameterDto>(x => x.Parameters)
                .WithRequired()
                .HasForeignKey(x => x.DeployTaskId);
        }
    }

    public class DeployUnitParameterDtoConfiguration : EntityTypeConfiguration<DeployTaskParameterDto>
    {
        public DeployUnitParameterDtoConfiguration()
        {
            ToTable("DeployTasksParameters");
        }
    }

    public class DeploymentDtoConfiguration : EntityTypeConfiguration<DeploymentDto>
    {
        public DeploymentDtoConfiguration()
        {
            ToTable("Deployments");
            HasMany(x => x.Logs)
                .WithRequired()
                .HasForeignKey(x => x.DeploymentId);
        }
    }
}