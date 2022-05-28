using CaloriesCounterAppFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class BaseViewModel : LoginViewModel
    {
        public CurrentUser UserData;
    }
    public class CurrentUser
    {
        public string Name;
        public string ConsumedCalories;
    }
}