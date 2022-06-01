using System;

namespace CaloriesCounterAppFx.Models
{
    public class ConsumedCaloriesHistoryViewModel
    {
        //Class used as a Model to send data about history to My Diary View
        //Tomasz Grabowski 22/05/2022
        public DateTime Date { get; set; }
        public double EnergySumOnDate { get; set; }
    }
}