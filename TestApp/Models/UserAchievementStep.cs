using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class UserAchievementStep
{
    public long StepId { get; set; }

    public long UserId { get; set; }

    public string DateEarned { get; set; }

    public virtual User User { get; set; }
}
