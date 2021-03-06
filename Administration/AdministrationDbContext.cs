using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Administration
{
    public class AdministrationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options) 
            : base(options)
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Throw);
        }
    }
}
