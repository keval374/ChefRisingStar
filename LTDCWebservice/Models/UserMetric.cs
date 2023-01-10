using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class UserMetric
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long MetricId { get; set; }

    public string ActivityTime { get; set; }

    public virtual Metric Metric { get; set; }

    public virtual User User { get; set; }
}
