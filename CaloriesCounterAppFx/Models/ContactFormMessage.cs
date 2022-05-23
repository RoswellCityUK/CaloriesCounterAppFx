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
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string From { get; set; }
        public string To { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; }
        public DateTime SendingDate { get; set; }

    }
}