using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class ContactFormMessage
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "We have to know who send the message!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Your email address")]
        public string From { get; set; }
        public string To { get; set; }
        [Required(ErrorMessage = "And don't forget to tell us what you are going to talk about...")]
        [DataType(DataType.Text)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "So say it... just use plain and short words, our monkeys are just starting their training for customer support.")]
        [DataType(DataType.Text)]
        public string Message { get; set; }
        public DateTime SendingDate { get; set; }

    }
}