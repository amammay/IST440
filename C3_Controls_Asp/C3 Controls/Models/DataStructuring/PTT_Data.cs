using System.Collections.Generic;
using System.Web.Configuration;

namespace C3_Controls.Models.DataStructuring
{
    public class PTT_Data
    {
        public PTT_Data()
        {
            if (CmSetting != "internal")
            {
                var myCouchDbConnector = new CouchDbConnector();

                PttMap = new Dictionary<string, PTTItem[]>();

                //Assign our dictionary to the one that was populated when the connection was made 
                PttMap = myCouchDbConnector.PttMap;
            }

            Voltages = new List<PricedItem>();

            //Cycle over our wtl map 
            foreach (var valueSet in PttMap)
            {
                var valueSetItems = new List<PTTItem>();

                //TODO document
                foreach (var valueSetItem in valueSet.Value)
                    valueSetItems.Add(valueSetItem);


                switch (valueSet.Key)
                {
                    case "Full_Voltage":


                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku,
                                Img = "img_voltages.png"
                            };
                            //Add her to the list
                            Voltages.Add(singleItem);
                        }
                        break;

                    case "Transformer (50/60 Hz)":
                        //Voltages = new List<PricedItem>();

                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku,
                                Img = "img_voltages.png"
                            };
                            //Add her to the list
                            Voltages.Add(singleItem);
                        }
                        break;

                    case "Resistor":
                        // Voltages = new List<PricedItem>();

                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku,
                                Img = "img_voltages.png"
                            };
                            //Add her to the list
                            Voltages.Add(singleItem);
                        }
                        break;

                    case "Lamp Type And Color":
                        LampTypeColor = new List<PricedItem>();

                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            LampTypeColor.Add(singleItem);
                        }
                        break;
                    case "Clamp Ring":
                        ClampRing = new List<PricedItem>();

                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            ClampRing.Add(singleItem);
                        }
                        break;
                    case "Lens Type":
                        LensType = new List<PricedItem>();

                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            LensType.Add(singleItem);
                        }
                        break;
                    case "Lens Color":
                        LensColor = new List<PricedItem>();

                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            LensColor.Add(singleItem);
                        }
                        break;

                    case "Options":
                        foreach (var item in valueSetItems)
                            Options = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                        break;
                }
            }
        }

        public Dictionary<string, PTTItem[]> PttMap { get; set; }
        public string CmSetting => WebConfigurationManager.AppSettings["CurrentDatebase"];


        public List<PricedItem> Voltages { get; set; }
        public List<PricedItem> LampTypeColor { get; set; }
        public List<PricedItem> ClampRing { get; set; }
        public List<PricedItem> LensType { get; set; }
        public List<PricedItem> LensColor { get; set; }
        public PricedItem Options { get; set; }
    }
}