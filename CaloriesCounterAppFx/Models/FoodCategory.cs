using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaloriesCounterAppFx.Models
{
    //Entity Class for Food Category
    public class FoodCategory
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
        public virtual List<Food> Foods { get; set; }
    }
}