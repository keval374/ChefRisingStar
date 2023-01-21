using LTDCWebservice.Models;
using LTDCWebservice.Utilities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LTDCWebservice.Authentication
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {   
        private string _key;

        public JWTAuthenticationManager(string key)
        {   
            _key = key;
        }

        public string Authenticate(string username, string password)
        {
            username = username.ToLower();
            User user = null;
            
            using(LtdcContext context = new LtdcContext())
            {
                user = context.Users.FirstOrDefault(u => u.UserName == username || u.Email == username);
            }

            if (user == null || string.IsNullOrEmpty(user.Salt) || string.IsNullOrEmpty(user.PasswordHash))
            {
                return null;
            }

            byte[] salt = Convert.FromHexString(user.Salt);
            string hash = HashUtility.HashPaswordWithSalt(password, salt);

            if(hash != user.PasswordHash)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin300")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256),
            };

            try
            {   
                using (LtdcContext context = new LtdcContext())
                {
                    context.Users.Update(user);
                    user.LastLoginDate = DateTime.UtcNow.ToString("O");
                    context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                //TODO: Logging
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
