using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class Team
{
    public long Id { get; set; }

    public string Name { get; set; }

    public long? CaptainId { get; set; }

    public long? Active { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
