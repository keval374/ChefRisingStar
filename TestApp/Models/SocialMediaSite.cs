using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class SocialMediaSite
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public virtual ICollection<UserSocialMediaSite> UserSocialMediaSites { get; } = new List<UserSocialMediaSite>();
}
