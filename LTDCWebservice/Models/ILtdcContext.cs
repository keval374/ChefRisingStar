using Microsoft.EntityFrameworkCore;

namespace LTDCWebservice.Models
{
    public interface ILtdcContext
    {
        DbSet<Achievement> Achievements { get; set; }
        DbSet<AchievementStep> AchievementSteps { get; set; }
        DbSet<FavoriteType> FavoriteTypes { get; set; }
        DbSet<Metric> Metrics { get; set; }
        DbSet<School> Schools { get; set; }
        DbSet<SocialMediaSite> SocialMediaSites { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<UserAchievement> UserAchievements { get; set; }
        DbSet<UserAchievementStep> UserAchievementSteps { get; set; }
        DbSet<UserFavortite> UserFavortites { get; set; }
        DbSet<UserMetric> UserMetrics { get; set; }
        DbSet<UserRecipe> UserRecipes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserSocialMediaSite> UserSocialMediaSites { get; set; }
    }
}