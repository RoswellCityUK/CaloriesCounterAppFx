using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class ConsumedCalories
    {
        //Class responsible for keeping all the data about calories consumed by the User
        //Tomasz Grabowski 22/05/2022
        public int Id { get; set; }
        public virtual Food Food { get; set; }
        public int Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}