using System;
using System.Collections.Generic;

namespace ChefRisingStar.DTOs
{
    public partial class UserDto
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public long? SchoolId { get; set; }

        public long? TeamId { get; set; }

        public string JoinDate { get; set; }

        public string LastLoginDate { get; set; }

        public int IsAdministrator { get; set; }

        public long? IsLocked { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }
    }
}