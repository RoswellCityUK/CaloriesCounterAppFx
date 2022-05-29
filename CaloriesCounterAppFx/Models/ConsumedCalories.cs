using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class ConsumedCalories
    {
        public int Id { get; set; }
        public virtual Food Food { get; set; }
        public int Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
    public class ConsumedCaloriesHistory
    {
        public DateTime Date { get; set; }
        public double EnergySumOnDate { get; set; }
    }
}