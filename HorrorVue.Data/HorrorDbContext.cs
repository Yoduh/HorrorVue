using HorrorVue.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace HorrorVue.Data
{
    public class HorrorDbContext : IdentityDbContext
    {
        public HorrorDbContext() { }

        public HorrorDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUserCollection>().HasKey(ac => new { ac.AppUserId, ac.CollectionId });

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Collection)
                .WithMany(c => c.Movies)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invite>()
                .HasOne(i => i.ToUser)
                .WithMany(a => a.SentInvites)
                .HasForeignKey(i => i.ToUserId);
            modelBuilder.Entity<Invite>()
                .HasOne(i => i.FromUser)
                .WithMany(a => a.ReceivedInvites)
                .HasForeignKey(i => i.FromUserId);
            modelBuilder.Entity<Invite>()
                .HasOne(i => i.Collection)
                .WithMany(c => c.Invites)
                .HasForeignKey(i => i.CollectionId);
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ranking> Rankings { get; set; }
        public virtual DbSet<Invite> Invite { get; set; }
    }
}
