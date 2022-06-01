using CaloriesCounterAppFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaloriesCounterAppFx.Models
{
    //View Model for passing data between My Diary Controller <-> View
    //Tomasz Grabowski 22/05/2022
    public class MyDiaryViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }
        public ApplicationUser User { get; set; }
        public double EnergyOnDay { get; set; }
        public double ProteinOnDay { get; set; }
        public double CarbsOnDay { get; set; }
        public double FatOnDay { get; set; }
        public double WaterOnDay { get; set; }
        public int WeightOnDay { get; set; }
        public List<ConsumedCalories> ConsumedCaloriesOnDay { get; set; }
        public List<ConsumedCaloriesHistoryViewModel> ConsumedCaloriesHistory { get; set; }
    }
}