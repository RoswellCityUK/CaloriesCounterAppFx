using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class FoodCatalogueIndexViewModel : BaseViewModel
    {
        public List<Food> Foods;
        public List<FoodCategory> Categories;
    }
}