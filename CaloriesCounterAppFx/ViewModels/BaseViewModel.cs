using CaloriesCounterAppFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    //Class used to create BaseViewModel which extends LoginViewModel.
    //It prevents controllers from not passing model for _LoginPartial View
    //Used for passing CurrentUser data for other Views.
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