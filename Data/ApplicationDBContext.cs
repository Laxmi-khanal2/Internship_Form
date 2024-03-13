﻿using InternshipForm.Models;
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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between PersonalInformation and RegisterUser
            modelBuilder.Entity<PersonalInformation>()
                .HasOne(pi => pi.RegisterUser)
                .WithMany()
                .HasForeignKey(pi => pi.RegisterUserId)
                .IsRequired();
        }
    }


    }

