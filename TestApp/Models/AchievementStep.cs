using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class AchievementStep
{
    public long StepId { get; set; }

    public long AchievementId { get; set; }

    public long? StepOrder { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual Achievement Achievement { get; set; }
}
