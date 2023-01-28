using ChefRisingStar.DTOs;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string SocialMediaAccount { get; set; }
        public string SocialMediaProvider { get; set; }
        public int SchoolId { get; set; }
        public int TeamId { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public string PreferredLanguage { get; set; }

        public bool IsAdministrator { get; set; }
        public bool IsLocked { get; set; }

        [JsonIgnore]
        public string DisplayName
        {
            get { return $"{UserName} - {FirstName} {LastName}"; }
        }

        public User()
        {

        }

        public User(int id, string firstName, string lastName, string userName, string emailAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = emailAddress;
            LastLoginDate = DateTime.UtcNow;

            SocialMediaAccount = string.Empty;
            SocialMediaProvider = string.Empty;
        }
        
        public User(string firstName, string lastName, string userName, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = emailAddress;
            LastLoginDate = DateTime.Now;

            JoinDate = DateTime.UtcNow;

            SocialMediaAccount = string.Empty;
            SocialMediaProvider = string.Empty;
            IsLocked = true;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Email))
                return UserName;

            return $"{UserName} : {Email}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {UserName}";
        }

        public static explicit operator UserDto(User user)
        {
            var item = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                IsAdministrator = user.IsAdministrator ? 1 : 0,
                IsLocked = user.IsLocked ? 1 : 0,
                JoinDate = user.JoinDate.ToString("O"),
                LastLoginDate = user.LastLoginDate.ToString("O"),
                PasswordHash = user.PasswordHash,
                Salt= user.Salt,
                SchoolId= user.SchoolId,
                TeamId= user.TeamId
            };

            if(item.SchoolId == 0)
            {
                item.SchoolId = null;
            }
            
            if(item.TeamId == 0)
            {
                item.TeamId = null;
            }

            return item;
        }

    }
}