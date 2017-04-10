using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetBcBackend.Models;

namespace DotNetBcBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<MassEmail> MassEmails { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Evid)
                    .HasName("PK_Event");

                entity.Property(e => e.Evid).HasColumnName("Evid");
            });

            builder.Entity<Sponsor>(entity =>
            {
                entity.HasKey(e => e.Sponid)
                    .HasName("PK_Sponsor");

                entity.Property(e => e.Sponid).HasColumnName("Sponid");
            });

            
            builder.Entity<MassEmail>(entity =>
            {
                entity.HasKey(e => new { e.Emid } )
                    .HasName("PK_MassEmail");

                entity.Property(e => e.Emid).HasColumnName("Emid");
            });
            

            builder.Entity<Speaker>(entity =>
            {
                entity.HasKey(e => e.Speakerid)
                    .HasName("PK_Speaker");

                entity.Property(e => e.Speakerid).HasColumnName("Speakerid");
            });

            builder.Entity<UserEvents>(entity =>
            {
                entity.HasKey(e => e.Usereventid)
                    .HasName("PK_UserEvents");

                entity.Property(e => e.Usereventid).HasColumnName("Usereventid");
            });

        }
    }
}
