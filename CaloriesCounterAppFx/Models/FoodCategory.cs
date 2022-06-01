using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaloriesCounterAppFx.Models
{
    public class FoodCategory
    {
        //Tomasz Grabowski 22/05/2022
        //Entity Class for Food Category
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public virtual List<Food> Foods { get; set; }
    }
}