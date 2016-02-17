using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserProfileVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string StreetNum { get; set; }
        public string StreetName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }


        public UserProfileVM() { }
        
    }
}