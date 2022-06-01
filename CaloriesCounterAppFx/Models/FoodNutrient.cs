using System.ComponentModel.DataAnnotations;

namespace CaloriesCounterAppFx.Models
{
    public class FoodNutrient
    {
        //Entity Class for Food Nutrient data
        //Tomasz Grabowski 22/05/2022
        [Key]
        public int Id { get; set; }
        public virtual Food Food { get; set; }
        public double WaterG { get; set; }
        public double EnergKcal { get; set; }
        public double ProteinG { get; set; }
        public double LipidTotG { get; set; }
        public double CarbohydrtG { get; set; }
        public double FiberTDG { get; set; }
        public double SugarTotG { get; set; }
        public double CalciumMg { get; set; }
        public double IronMg { get; set; }
        public double MagnesiumMg { get; set; }
        public double PhosphorusMg { get; set; }
        public double PotassiumMg { get; set; }
        public double SodiumMg { get; set; }
        public double ZincMg { get; set; }
        public double CopperMg { get; set; }
        public double ManganeseMg { get; set; }
        public double SeleniumΜg { get; set; }
        public double VitCMg { get; set; }
        public double NiacinMg { get; set; }
        public double VitB6Mg { get; set; }
        public double VitB12Μg { get; set; }
        public double VitAIU { get; set; }
        public double VitARAE { get; set; }
        public double RetinolΜg { get; set; }
        public double AlphaCarotΜg { get; set; }
        public double BetaCarotΜg { get; set; }
        public double BetaCryptΜg { get; set; }
        public double VitEMg { get; set; }
        public double VitDΜg { get; set; }
        public double VitDIU { get; set; }
        public double VitKΜg { get; set; }
        public double CholestrlMg { get; set; }
    }
}