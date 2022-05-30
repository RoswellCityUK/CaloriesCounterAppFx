using System;

namespace CaloriesCounterAppFx.Models
{
    //Class used as a Model to send data about history to My Diary View
    public class ConsumedCaloriesHistoryViewModel
    {
        public DateTime Date { get; set; }
        public double EnergySumOnDate { get; set; }
    }
}