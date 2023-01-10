using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class FavoriteType
{
    public long Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<UserFavortite> UserFavortites { get; } = new List<UserFavortite>();
}
