using System.Collections.Generic;
using C3_Controls.Models.DataStructuring;

namespace C3_Controls.Models.UiDataModel
{
    /// <summary>
    ///     This is the Tower Light product. It contains information
    ///     pertaining to only the Tower Light.
    /// </summary>
    public class TowerLight : Control
    {
        //Get instance of WtlData so we can set our data context
        public TowerLight()
        {
            WtlData = new WTL_Data();
        }

        public override PricedItem OperatorType => WtlData.OperatorType;
        public override List<PricedItem> Voltages => WtlData.Voltages;
        public PricedItem ModuleDiameter => WtlData.ModuelDiameter;
        public List<PricedItem> Styles => WtlData.Styles;
        public List<PricedItem> Positions => WtlData.Positions;
        public List<PricedItem> SoundModule => WtlData.SoundModule;

       
    }


    /// <summary>
    ///     This is the Push-to-Test button product. It contains
    ///     infomation only pertaining to the push-to-test button.
    /// </summary>
    public class PushToTest : Control
    {
        //Get instance of PttData to set our data context
        public PushToTest()
        {
            PttData = new PTT_Data();
        }

        public override PricedItem OperatorType => new PricedItem() { Name = "Push to Test", Sku = "PTT", Price = DEFAULT_PRICE };
        public override List<PricedItem> Voltages => PttData.Voltages;
        public List<PricedItem> BasicOperators => PttData.BasicOperators;
        public List<PricedItem> LampColors => PttData.LampColors;
        public List<PricedItem> ClampRings => PttData.ClampRings;
        public List<PricedItem> LensTypes => PttData.LensTypes;
        public List<PricedItem> LensColors => PttData.LensColors;
        public List<PricedItem> Options => PttData.Options;

    }
}