using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class FoodCatalogueIndexViewModel : BaseViewModel
    {
        public List<Food> Foods { get; set; }
        public List<FoodCategory> Categories { get; set; }
    }
    public class AddToDiaryViewModel
    {
        [Required]
        public int FoodId { get; set; }
        [Required]
        public int PortionAmount { get; set; }
    }
}