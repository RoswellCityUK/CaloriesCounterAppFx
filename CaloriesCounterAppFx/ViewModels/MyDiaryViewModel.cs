using CaloriesCounterAppFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Models
{
    public class MyDiaryViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }
        public ApplicationUser User { get; set; }
        public List<ConsumedCalories> ConsumedCaloriesOnDay { get; set; }
        public List<ConsumedCaloriesHistory> ConsumedCaloriesHistory { get; set; }
    }
}