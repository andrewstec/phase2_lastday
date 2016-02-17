using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Organic_Launch;

namespace WebApplication1.Models
{
    public class FarmerProfileVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string StreetNum { get; set; }
        public string StreetName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string farmName { get; set; }
        public string farmProfile { get; set; }
        public FarmerProfileVM() { }
    }
}