using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    //Class responsible for keeping all the data about calories consumed by the User
    public class ConsumedCalories
    {
        public int Id { get; set; }
        public virtual Food Food { get; set; }
        public int Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}