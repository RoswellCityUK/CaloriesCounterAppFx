﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    //View Model for Food Catalog
    public class FoodCatalogueIndexViewModel : BaseViewModel
    {
        public List<Food> Foods { get; set; }
        public List<FoodCategory> Categories { get; set; }
    }
    //View Model for Food Diary Adding Form
    public class AddToDiaryViewModel
    {
        [Required]
        public int FoodId { get; set; }
        [Required]
        [Display(Name = "Portion")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number bigger than 0")]
        public int PortionAmount { get; set; }
    }
}