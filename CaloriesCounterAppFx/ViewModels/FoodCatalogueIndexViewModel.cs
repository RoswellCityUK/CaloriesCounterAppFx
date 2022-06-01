using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class FoodCatalogueIndexViewModel : BaseViewModel
    {
        //View Model for Food Catalog
        //Tomasz Grabowski 22/05/2022
        public List<Food> Foods { get; set; }
        public List<FoodCategory> Categories { get; set; }
    }
    public class AddToDiaryViewModel
    {
        //View Model for Food Diary Adding Form
        //Tomasz Grabowski 22/05/2022
        [Required]
        public int FoodId { get; set; }
        [Required]
        [Display(Name = "Portion")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number bigger than 0")]
        public int PortionAmount { get; set; }
    }
}