
using Data.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<MemberEntity>(options)
{
    public virtual DbSet<MemberAdressEntity> MemberAdresses { get; set; }
    public virtual DbSet<ClientEntity> Clients { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }
    public virtual DbSet<ProjectMemberEntity> ProjectMembers { get; set; }
    public virtual DbSet<StatusEntity> Statuses { get; set; }


    //I denna del av koden har jag tagit hjälp av ChatGPT. Koden skapar relationer mellan entiteter i databasen.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // 1-1 relation -en användare har en adress
        modelBuilder.Entity<MemberEntity>()
            .HasOne(m => m.MemberAdress)
            .WithOne(a => a.Member)
            .HasForeignKey<MemberAdressEntity>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1-många reation -en kund kan ha flera projekt, ett projekt har endast en kund
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
          .HasOne(p => p.Status)
          .WithMany(c => c.Projects)
          .HasForeignKey(p => p.StatusId)
          .OnDelete(DeleteBehavior.Restrict);

        // Mellantabell på grund av många-många relation, en användare kan vara med i flera projekt och ett projekt kan ha flera användare
        modelBuilder.Entity<ProjectMemberEntity>()
            .HasKey(pm => new { pm.ProjectId, pm.MemberId });

        modelBuilder.Entity<ProjectMemberEntity>()
            .HasOne(pm => pm.Project)
            .WithMany(p => p.ProjectMembers)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<ProjectMemberEntity>()
            .HasOne(pm => pm.Member)
            .WithMany(m => m.ProjectMembers)
            .HasForeignKey(pm => pm.MemberId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}

  
