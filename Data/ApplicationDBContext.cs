using InternshipForm.Models;
using Microsoft.EntityFrameworkCore;
using InternshipForm.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InternshipForm.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<OfficalUse> OfficalUse { get; set; }
        public DbSet<PersonalInformation> PersonalInformation { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<GuardianDetails> GuardianDetails { get; set; }
        public DbSet<References> References { get; set; }

        public DbSet<RegisterUser> RegisterUser { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }
        public DbSet<CreateInternship> CreateInternship { get; set; }
        public DbSet<RegisterCompany> RegisterCompany { get; set; }
        public DbSet<AppliedInternships>AppliedInternships { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between PersonalInformation and RegisterUser
            modelBuilder.Entity<PersonalInformation>()
                .HasOne(pi => pi.RegisterUser)
                .WithMany()
                .HasForeignKey(pi => pi.RegisterUserId)
                .IsRequired();

            // Define the relationship between AppliedInternships and RegisterUser
            modelBuilder.Entity<AppliedInternships>()
                .HasOne(ai => ai.RegisterUser)
                .WithMany(ru => ru.AppliedInternships)
                .HasForeignKey(ai => ai.RegisterUserId)
                .IsRequired();

            // Define the relationship between AppliedInternships and PersonalInformation
            modelBuilder.Entity<AppliedInternships>()
                .HasOne(ai => ai.PersonalInformation)
                .WithMany(pi => pi.AppliedInternships)
                .HasForeignKey(ai => ai.InternId)
                .IsRequired();

            // Optionally, if you have a relationship between CreateInternship and AppliedInternships:
            modelBuilder.Entity<AppliedInternships>()
                .HasOne(ai => ai.CreateInternship)
                .WithMany(ci => ci.AppliedInternships)
                .HasForeignKey(ai => ai.InternshipId)
                .IsRequired();
        }



    }

}

