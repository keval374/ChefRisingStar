﻿using System;

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
        public int SchoolId { get; set; }
        public int TeamID { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public bool IsAdmin { get; set; }

        public User()
        {

        }
        
        public User(int id, string firstName, string lastName, string userName, string emailAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            EmailAddress = emailAddress;
            LastLoginDate = DateTime.Now;

            JoinDate = DateTime.Now.AddMonths(-4);

            SocialMediaAccount = string.Empty;
            SocialMediaProvider = string.Empty;
        }

        public override string ToString()
        {
            return $"{UserName} : {EmailAddress}";
        }
    }
}