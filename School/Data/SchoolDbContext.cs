using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Models.Entities;

namespace School.Data
{
  public class SchoolDbContext : DbContext
  {
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    
    public DbSet<Students> Students { get; set; }
    public DbSet<StudentClass> StudentClass { get; set; }
    public DbSet<Gender> Gender { get; set; }
    public DbSet<Subjects> Subjects { get; set; }
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<StudentSubject> StudentSubject { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<Teacher>()
      //  .HasOne(t => t.Subject)
      //  .WithOne(s => s.teacher)
      //  .HasForeignKey<Teacher>(i => i.SubjectId);

     

    }
  }

 
}
