using System;

namespace ChefRisingStar.Models
{
    public enum MetricType
    {
        NewUserCreated,
        UserLoggedIn,
        UserLinkedSocialMediaAccount,
        UserCompletedAchievement,
        UserSearchedRecipe,
        UserLikedRecipe,
        UserLikedIngredient,
        UserSharedPhoto,
        UserSharedUpdate,
        UserJoinedBrigade,
        UserSubmittedRecipe,
        UserSentMessage,
        AppError
    }
    public class AppMetric
    {
        public MetricType MetricTypeCaptured { get; set; }
        public string Value { get; set; }
        public DateTime ActivityTime { get; set; }

        public int UserId { get; set; }

        public AppMetric()
        {
        }

        public AppMetric(MetricType metricTypeCaptured, int userId, string value)
        {
            MetricTypeCaptured = metricTypeCaptured;
            UserId = userId;
            Value = value;
            ActivityTime = DateTime.UtcNow;
        }

        public static explicit operator UserMetric(AppMetric appMetric)
        {
            return new UserMetric()
            {
                ActivityTime = appMetric.ActivityTime.ToString("O"),
                UserId = appMetric.UserId,
                MetricId = (int)appMetric.MetricTypeCaptured
            };
        }
    }
}
