using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LTDCWebservice.Models;

public partial class Metric
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [IgnoreDataMember]
    [JsonIgnore]
    public virtual ICollection<UserMetric> UserMetrics { get; } = new List<UserMetric>();
}
