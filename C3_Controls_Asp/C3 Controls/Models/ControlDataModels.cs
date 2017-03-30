using System.Collections.Generic;
using C3_Controls.Models.DataStructuring;

namespace C3_Controls.Models
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
        public List<PricedItem> LightLens => WtlData.LightLens;
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

        public override PricedItem OperatorType
            => new PricedItem {Name = "Push to Test", Sku = "PTT", Price = DEFAULT_PRICE};

        public override List<PricedItem> Voltages => PttData.Voltages;
        public List<PricedItem> LampColors => PttData.LampTypeColor;
        public List<PricedItem> ClampRings => PttData.ClampRing;
        public List<PricedItem> LensTypes => PttData.LensType;
        public List<PricedItem> LensColors => PttData.LensColor;
        public PricedItem Options => PttData.Options;
    }
}