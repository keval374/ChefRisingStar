using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LTDCWebservice.Models;

public partial class LtdcContext : DbContext, ILtdcContext
{
    public LtdcContext()
    {
    }

    public LtdcContext(DbContextOptions<LtdcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<AchievementStep> AchievementSteps { get; set; }

    public virtual DbSet<FavoriteType> FavoriteTypes { get; set; }

    public virtual DbSet<Metric> Metrics { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<SocialMediaSite> SocialMediaSites { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAchievement> UserAchievements { get; set; }

    public virtual DbSet<UserAchievementStep> UserAchievementSteps { get; set; }

    public virtual DbSet<UserFavortite> UserFavortites { get; set; }

    public virtual DbSet<UserMetric> UserMetrics { get; set; }

    public virtual DbSet<UserRecipe> UserRecipes { get; set; }

    public virtual DbSet<UserSocialMediaSite> UserSocialMediaSites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=.\\Database\\ltdc.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<AchievementStep>(entity =>
        {
            entity.HasKey(e => e.StepId);

            entity.Property(e => e.StepId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StepID");
            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
            entity.Property(e => e.Name).IsRequired();

            entity.HasOne(d => d.Achievement).WithMany(p => p.AchievementSteps)
                .HasForeignKey(d => d.AchievementId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FavoriteType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Metric>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Metrics_Name").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<SocialMediaSite>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_SocialMediaSites_Name").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Url).IsRequired();
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.UserName).IsRequired();

            entity.HasOne(d => d.School).WithMany(p => p.Users).HasForeignKey(d => d.SchoolId);

            entity.HasOne(d => d.Team).WithMany(p => p.Users).HasForeignKey(d => d.TeamId);
        });

        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AchievementId });

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
            entity.Property(e => e.DateEarned).IsRequired();

            entity.HasOne(d => d.Achievement).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.AchievementId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserAchievementStep>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.StepId });

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.StepId).HasColumnName("StepID");
            entity.Property(e => e.DateEarned).IsRequired();

            entity.HasOne(d => d.User).WithMany(p => p.UserAchievementSteps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserFavortite>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RefId });

            entity.Property(e => e.ActivityTime).IsRequired();
            entity.Property(e => e.FavoriteTypeId).HasColumnName("FavoriteTypeID");

            entity.HasOne(d => d.FavoriteType).WithMany(p => p.UserFavortites)
                .HasForeignKey(d => d.FavoriteTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserFavortites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserMetric>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ActivityTime).IsRequired();
            entity.Property(e => e.MetricId).HasColumnName("MetricID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Metric).WithMany(p => p.UserMetrics)
                .HasForeignKey(d => d.MetricId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //todo: lookups by id
            //entity.HasOne(d => d.User).WithMany(p => p.UserMetrics)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserRecipe>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.SpoonacularId).IsRequired();

            entity.HasOne(d => d.User).WithMany(p => p.UserRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserSocialMediaSite>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SocialMediaSiteId });

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AccountName).IsRequired();

            entity.HasOne(d => d.SocialMediaSite).WithMany(p => p.UserSocialMediaSites)
                .HasForeignKey(d => d.SocialMediaSiteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserSocialMediaSites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
