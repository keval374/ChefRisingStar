using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class UserFavortite
{
    public long UserId { get; set; }

    public string RefId { get; set; }

    public long FavoriteTypeId { get; set; }

    public string ActivityTime { get; set; }

    public virtual FavoriteType FavoriteType { get; set; }

    public virtual User User { get; set; }
}
