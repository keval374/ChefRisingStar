using LTDCWebservice.Models;
using LTDCWebservice.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LTDCWebservice.Handlers
{
    internal class UserHandler
    {
        private readonly LtdcContext _context;

        internal UserHandler(LtdcContext context)
        {
            _context = context;
        }

        internal IActionResult CreateAccount(User user)
        {
            var FoundUser = _context.Users.Any(u => u.Email == user.Email || u.UserName == user.UserName);

            if (FoundUser || string.IsNullOrEmpty(user.PasswordHash))
            {
                return new BadRequestResult();
            }
            
            //Done on client side for now
            //string salt;
            //string hash = HashUtility.HashPasword(user.PasswordHash, out salt);

            //user.PasswordHash = hash;
            //user.Salt = salt;

            return AddUser(user);
        }

        internal IActionResult AddUser(User user) 
        {
            try
            {
                var FoundUser = _context.Users.Any(u => u.Email == user.Email || u.UserName == user.UserName);

                if (FoundUser || user.Id != 0)
                {
                    return new BadRequestResult();
                }

                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Email))
                {
                    return new BadRequestResult();
                }

                if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.Salt))
                {
                    user.IsLocked = 1;
                }

                _context.Users.Add(user);
                _context.SaveChangesAsync();
                return new OkObjectResult(user);
            }
            catch(Exception ex)
            {
                //TODO: Logging
                return new BadRequestResult();
            }
        }
    }
}
