using System.Collections.Generic;
using System.Web.Mvc;
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
}