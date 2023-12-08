using Microsoft.EntityFrameworkCore;

namespace Transport_x.Entities
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ShippingType> ShippingTypes { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>()
                .HasOne(g => g.Bill) // Goods chỉ có một Bill liên quan
                .WithMany()          // Bill có thể có nhiều Goods liên quan
                .HasForeignKey(g => g.IdBill); // Khóa ngoại trong Goods là IdBill
        }
    }
}
