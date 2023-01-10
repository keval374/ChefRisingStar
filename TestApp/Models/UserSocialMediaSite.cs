using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class UserSocialMediaSite
{
    public long UserId { get; set; }

    public long SocialMediaSiteId { get; set; }

    public string AccountName { get; set; }

    public virtual SocialMediaSite SocialMediaSite { get; set; }

    public virtual User User { get; set; }
}
