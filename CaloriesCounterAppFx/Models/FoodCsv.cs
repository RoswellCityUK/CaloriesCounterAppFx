using CsvHelper.Configuration;
using System;
using System.Linq;
using System.Web;

namespace CaloriesCounterAppFx.Models
{

    public class FoodCsv
    {
        public string ShrtDesc { get; set; }
        public string Category { get; set; }
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
    public class FoodCsvClassMap : ClassMap<FoodCsv>
    {
        public FoodCsvClassMap()
        {
            Map(m => m.ShrtDesc).Name("Shrt_Desc");
            Map(m => m.Category).Name("Category");
            Map(m => m.WaterG).Name("Water_(g)");
            Map(m => m.EnergKcal).Name("Energ_Kcal");
            Map(m => m.ProteinG).Name("Protein_(g)");
            Map(m => m.LipidTotG).Name("Lipid_Tot_(g)");
            Map(m => m.CarbohydrtG).Name("Carbohydrt_(g)");
            Map(m => m.FiberTDG).Name("Fiber_TD_(g)");
            Map(m => m.SugarTotG).Name("Sugar_Tot_(g)");
            Map(m => m.CalciumMg).Name("Calcium_(mg)");
            Map(m => m.IronMg).Name("Iron_(mg)");
            Map(m => m.MagnesiumMg).Name("Magnesium_(mg)");
            Map(m => m.PhosphorusMg).Name("Phosphorus_(mg)");
            Map(m => m.PotassiumMg).Name("Potassium_(mg)");
            Map(m => m.SodiumMg).Name("Sodium_(mg)");
            Map(m => m.ZincMg).Name("Zinc_(mg)");
            Map(m => m.CopperMg).Name("Copper_mg)");
            Map(m => m.ManganeseMg).Name("Manganese_(mg)");
            Map(m => m.SeleniumΜg).Name("Selenium_(µg)");
            Map(m => m.VitCMg).Name("Vit_C_(mg)");
            Map(m => m.NiacinMg).Name("Niacin_(mg)");
            Map(m => m.VitB6Mg).Name("Vit_B6_(mg)");
            Map(m => m.VitB12Μg).Name("Vit_B12_(µg)");
            Map(m => m.VitAIU).Name("Vit_A_IU");
            Map(m => m.VitARAE).Name("Vit_A_RAE");
            Map(m => m.RetinolΜg).Name("Retinol_(µg)");
            Map(m => m.AlphaCarotΜg).Name("Alpha_Carot_(µg)");
            Map(m => m.BetaCarotΜg).Name("Beta_Carot_(µg)");
            Map(m => m.BetaCryptΜg).Name("Beta_Crypt_(µg)");
            Map(m => m.VitEMg).Name("Vit_E_(mg)");
            Map(m => m.VitDΜg).Name("Vit_D_µg");
            Map(m => m.VitDIU).Name("Vit_D_IU");
            Map(m => m.VitKΜg).Name("Vit_K_(µg)");
            Map(m => m.CholestrlMg).Name("Cholestrl_(mg)");
        }
    }
}