using Microsoft.AspNetCore.Mvc;
using System.Linq;
using my_contacts_api.Context;
using my_contacts_api.Models;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace my_contacts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ContactsAppContext _context;
        public UsersController(ContactsAppContext context) { _context = context; }

        [HttpGet]
        public List<Users> GetUsers()
        {
            return _context.Users.Where(u => u.isRegistered).ToList();
        }

        [HttpPost("signin")]
        public string signIn([FromBody] LoginModel loginModel)
        {
            Users foundUser = _context.Users.FirstOrDefault(i => i.PhoneNumber == loginModel.PhoneNumber && i.Password == loginModel.Password && i.isRegistered);
            return (Login(foundUser));
        }

        [HttpPost]
        public bool AddUser(Users user)
        {
            Users foundUser = _context.Users.FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);
            if (foundUser != null)
            {
                if (foundUser.isRegistered)
                    return (false);
                foundUser.isRegistered = true;
                _context.Users.Update(foundUser);
            }
            else
                _context.Users.Add(user);
            _context.SaveChanges();
            return (true);
        }

        [HttpPut]
        public void UpdateUser(Users user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
            Users user = _context.Users.FirstOrDefault(i => i.Id == id);
            user.isRegistered = false;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public string Login(Users user)
        {
            // return null if user not found
            if (user == null)
                return string.Empty;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Startup.SECRET);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Name, user.LastName),
                    new Claim(ClaimTypes.Name, user.PhoneNumber),
                    new Claim(ClaimTypes.Name, user.Password)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token));
        }
    }

}
