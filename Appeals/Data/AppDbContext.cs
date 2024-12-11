using Microsoft.EntityFrameworkCore;
using Appeals.Models;

namespace Appeals.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options): base(options) { }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Appeal> Appeal { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Plot> Plots { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Address__3214EC271DF697D3");

                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Build)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(240)
                    .IsUnicode(false);
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.PlotId).HasColumnName("Plot_ID");
                entity.Property(e => e.Street)
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.HasOne(d => d.Plot).WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.PlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Plot");
            });

            modelBuilder.Entity<Appeal>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Appeal__3214EC2726B68D14");

                entity.ToTable("Appeal");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CreationDate).HasColumnName("Creation_date");
                entity.Property(e => e.DecisionDate).HasColumnName("Decision_date");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.IdExecutor).HasColumnName("ID_executor");
                entity.Property(e => e.IdPlot).HasColumnName("ID_plot");
                entity.Property(e => e.IdResident).HasColumnName("ID_resident");
                entity.Property(e => e.IdStatus).HasColumnName("ID_status");
                entity.Property(e => e.IdTopic).HasColumnName("ID_topic");
                entity.Property(e => e.Text).HasColumnType("text");
                entity.Property(e => e.Title)
                    .HasMaxLength(240)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdExecutorNavigation).WithMany(p => p.AppealIdExecutorNavigations)
                    .HasForeignKey(d => d.IdExecutor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appeal_Executor");

                entity.HasOne(d => d.IdPlotNavigation).WithMany(p => p.Appeals)
                    .HasForeignKey(d => d.IdPlot)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appeal_Plot");

                entity.HasOne(d => d.IdResidentNavigation).WithMany(p => p.AppealIdResidentNavigations)
                    .HasForeignKey(d => d.IdResident)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appeal_Resident");

                entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Appeals)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appeal_Status");

                entity.HasOne(d => d.IdTopicNavigation).WithMany(p => p.Appeals)
                    .HasForeignKey(d => d.IdTopic)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appeal_Topic");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__History__3214EC2726A4B742");

                entity.ToTable("History");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Change_date");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.IdAppeal).HasColumnName("ID_appeal");
                entity.Property(e => e.IdUser).HasColumnName("ID_user");

                entity.HasOne(d => d.IdAppealNavigation).WithMany(p => p.Histories)
                    .HasForeignKey(d => d.IdAppeal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_Appeal");

                entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Histories)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_User");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Organiza__3214EC2796C95167");

                entity.ToTable("Organization");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Address)
                    .HasMaxLength(240)
                    .IsUnicode(false);
                entity.Property(e => e.ArchivingDate).HasColumnName("Archiving_date");
                entity.Property(e => e.CreateDate).HasColumnName("Create_date");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.Name)
                    .HasMaxLength(240)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Plot>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Plot__3214EC27C3EE05DA");

                entity.ToTable("Plot");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.OrganizationId).HasColumnName("Organization_ID");

                entity.HasOne(d => d.Organization).WithMany(p => p.Plots)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plot__Organizati__398D8EEE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC274CB19C38");

                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.Name)
                    .HasMaxLength(240)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Status__3214EC27F6C5B0EA");

                entity.ToTable("Status");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.Name)
                    .HasMaxLength(240)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Topic__3214EC2789B78C29");

                entity.ToTable("Topic");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(240)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__User__3214EC27B272B51D");

                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AddressId).HasColumnName("Address_ID");
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Deletion_date");
                entity.Property(e => e.Email)
                    .HasMaxLength(240)
                    .IsUnicode(false);
                entity.Property(e => e.Lastname)
                    .HasMaxLength(240)
                    .IsUnicode(false);
                entity.Property(e => e.Middlename)
                    .HasMaxLength(240)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(240)
                    .IsUnicode(false);
                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.HasOne(d => d.Address).WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_User_Address");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });
        }
    }
}
