﻿using Introduce.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Services.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleByUser> RoleByUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId)
                .HasColumnType("varchar(50)");

                entity.Property(e => e.UserName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

                entity.HasIndex(e => new { e.UserEmail })
                .IsUnique();
                entity.Property(e => e.UserEmail)
                .IsRequired()
                .HasColumnType("nvarchar(70)");

                entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

                entity.Property(e => e.JoinedDate)
                .IsRequired();
            });

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId)
                .HasColumnType("varchar(50)");

                entity.Property(e => e.RoleName)
                .IsRequired()
                .HasColumnType("nvarchar(50)");

                entity.Property(e => e.RolePriority)
                .IsRequired();
            });

            modelBuilder.Entity<RoleByUser>().ToTable("RoleByUser");
            modelBuilder.Entity<RoleByUser>()
                .HasKey(rbu =>
                new
                {
                    rbu.UserId,
                    rbu.RoleId,
                });

            modelBuilder.Entity<User>()
                .HasOne<RoleByUser>(u => u.RoleByUser)
                .WithOne(rbu => rbu.User)
                .HasForeignKey<RoleByUser>(rbu => rbu.UserId);

            modelBuilder.Entity<Role>()
                .HasMany<RoleByUser>(r => r.RoleByUsers)
                .WithOne(rbu => rbu.Role)
                .HasForeignKey(rbu => rbu.RoleId);
        }
    }
}