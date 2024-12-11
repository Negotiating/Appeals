using Microsoft.EntityFrameworkCore;
using Appeals.Models;

namespace Appeals.Data
{
    public class AppealsDbContext : DbContext
    {
        public AppealsDbContext (DbContextOptions<AppealsDbContext> options): base(options) { }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Appeal> Appeal { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appeal>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnName("Creation_date");
                entity.Property(e => e.DecisionDate).HasColumnName("Decision_date");
                entity.Property(e => e.IdStatus).HasColumnName("Id_status");
                entity.Property(e => e.IdExecutor).HasColumnName("Id_executor");
                entity.Property(e => e.IdResident).HasColumnName("Id_resident");
                entity.Property(e => e.IdPlot).HasColumnName("Id_plot");
                entity.Property(e => e.IdTopic).HasColumnName("Id_topic");
                entity.Property(e => e.DeletionDate).HasColumnName("Deletion_date");
            });
        }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Plot> Plots { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
