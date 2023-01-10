using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class UserAchievement
{
    public long AchievementId { get; set; }

    public long UserId { get; set; }

    public string DateEarned { get; set; }

    public virtual Achievement Achievement { get; set; }

    public virtual User User { get; set; }
}
