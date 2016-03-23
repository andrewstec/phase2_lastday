using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class Message
    {
        [DisplayAttribute(Name = "Your Email Address")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+"
                    + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                    + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                    + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                    + @"[a-zA-Z]{2,}))$", ErrorMessage = "Please Enter a Valid Email")]
        [Required]
        public string Sender { get; set; }

        [Required]
        public string Subject { get; set; }

        [DisplayAttribute(Name = "Message")]
        [Required]
        public string Body { get; set; }

        public Message() { }
        public Message(string sender, string body)
        {
            Sender = sender;
            Body = body;
        }
    }
}
