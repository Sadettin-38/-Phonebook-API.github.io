using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
namespace my_contacts_api.Models
{
    public class LoginModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }   
}


