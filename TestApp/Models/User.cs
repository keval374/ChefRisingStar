using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class User
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public long? SchoolId { get; set; }

    public long? TeamId { get; set; }

    public string JoinDate { get; set; }

    public string LastLoginDate { get; set; }

    public long? IsAdministrator { get; set; }

    public long? IsLocked { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }

    public virtual School School { get; set; }

    public virtual Team Team { get; set; }

    public virtual ICollection<UserAchievementStep> UserAchievementSteps { get; } = new List<UserAchievementStep>();

    public virtual ICollection<UserAchievement> UserAchievements { get; } = new List<UserAchievement>();

    public virtual ICollection<UserFavortite> UserFavortites { get; } = new List<UserFavortite>();

    public virtual ICollection<UserMetric> UserMetrics { get; } = new List<UserMetric>();

    public virtual ICollection<UserRecipe> UserRecipes { get; } = new List<UserRecipe>();

    public virtual ICollection<UserSocialMediaSite> UserSocialMediaSites { get; } = new List<UserSocialMediaSite>();
}
