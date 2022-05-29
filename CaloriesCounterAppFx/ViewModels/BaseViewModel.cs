using CaloriesCounterAppFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class BaseViewModel : LoginViewModel
    {
        public CurrentUser UserData { get; set; }
    }
    public class CurrentUser
    {
        public string Name { get; set; }
        public string ConsumedCalories { get; set; }
    }
}