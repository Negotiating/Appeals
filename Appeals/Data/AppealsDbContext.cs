using Microsoft.EntityFrameworkCore;
using Appeals.Models;

namespace Appeals.Data
{
    public class AppealsDbContext : DbContext
    {
        public AppealsDbContext (DbContextOptions<AppealsDbContext> options): base(options) { }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Appeal> Appeals { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Plot> Plots { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
