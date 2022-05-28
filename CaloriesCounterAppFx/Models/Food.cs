using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;

namespace CaloriesCounterAppFx.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual FoodCategory Category { get; set; }
        public virtual List<FoodNutrient> Nutrients { get; set; }
        public string GetLocalImage()
        {
            int folder = this.Id / 500;
            string localImagePath = "/Content/imgs/products/" + folder + "/" + this.Id + ".jpg";

            if (File.Exists(HttpContext.Current.Server.MapPath(localImagePath)))
            {
                return localImagePath;
            }

            return localImagePath;
        }
    }
}