using Microsoft.EntityFrameworkCore;
using StudentServiceStorage.Data;

namespace StudentServiceStorage
{
    public class PostgresContext : DbContext
    {
        string _connectionString;

        public DbSet<StudentDb> students { get; set; }
        public DbSet<GroupDb> groups { get; set; }
        public DbSet<StudentGroupDb> student_to_group { get; set; }

        public PostgresContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroupDb>()
                    .HasKey(bc => new { bc.student_id, bc.group_id });
            modelBuilder.Entity<StudentGroupDb>()
                .HasOne(bc => bc.student)
                .WithMany(b => b.student_groups)
                .HasForeignKey(bc => bc.student_id);
            modelBuilder.Entity<StudentGroupDb>()
                .HasOne(bc => bc.groupdb)
                .WithMany(c => c.student_groups)
                .HasForeignKey(bc => bc.group_id);
        }
    }
}
