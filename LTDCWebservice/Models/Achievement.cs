using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class Achievement
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<AchievementStep> AchievementSteps { get; } = new List<AchievementStep>();

    public virtual ICollection<UserAchievement> UserAchievements { get; } = new List<UserAchievement>();
}
