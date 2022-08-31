using System;
using System.Collections.Generic;
using System.Text;

namespace ChefRisingStar.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress{ get; set; }
        public string SocialMediaAccount { get; set; }
        public string SocialMediaProvider { get; set; }
        public bool IsAdmin { get; set; }

    }
}
