using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Configuration;
using C3_Controls.Models.DataStructuring;

namespace C3_Controls.Models
{
    /// <summary>
    ///     This is a base product and contains all the
    ///     information that is the same amongst all products.
    /// </summary>
    public abstract class Control
    {
        public static double DEFAULT_PRICE = 0.0;
        public abstract PricedItem OperatorType { get; }
        public abstract List<PricedItem> Voltages { get; }
        public WTL_Data WtlData { get; set; }
        public PTT_Data PttData { get; set; }

        // Converts a string list into a select list for drop downs
        public static IEnumerable<SelectListItem> Convert(List<string> items)
        {
            var listItems = new List<SelectListItem>();
            foreach (var item in items)
                listItems.Add(new SelectListItem {Text = item, Value = item});
            return listItems;
        }
        

    }


    /// <summary>
    ///     This is the Tower Light product. It contains information
    ///     pertaining to only the Tower Light.
    /// </summary>
    public class TowerLight : Control
    {

        public TowerLight()
        {
            WtlData = new WTL_Data();
        }

        public override PricedItem OperatorType => WtlData.OperatorType;

        public override List<PricedItem> Voltages => WtlData.Voltages;

        public PricedItem ModuleDiameter => WtlData.ModuelDiameter;

        public List<PricedItem> Styles => WtlData.Styles;

        public List<PricedItem> Positions => WtlData.Positions;
    }


    /// <summary>
    ///     This is the Push-to-Test button product. It contains
    ///     infomation only pertaining to the push-to-test button.
    /// </summary>
    public class PushToTest : Control
    {

        public PushToTest()
        {
            PttData = new PTT_Data();
        }

        public override PricedItem OperatorType => new PricedItem {Name = "Push to Test", Sku = "PTT", Price = DEFAULT_PRICE};

        public override List<PricedItem> Voltages => PttData.Voltages;

        public List<PricedItem> LampColors => PttData.LampTypeColor;

        public List<PricedItem> ClampRings => PttData.ClampRing;

        public List<PricedItem> LensTypes => PttData.LensType;

        public List<PricedItem> LensColors => PttData.LensColor;

        public PricedItem Options => PttData.Options;
    }
}