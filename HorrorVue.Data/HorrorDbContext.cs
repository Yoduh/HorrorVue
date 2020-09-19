﻿using HorrorVue.Data.Models;
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
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Ranking> Rankings { get; set; }

    }
}