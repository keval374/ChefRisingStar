using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class Metric
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<UserMetric> UserMetrics { get; } = new List<UserMetric>();
}
