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

                            //extra logic for assigning image path to position
                            if (singleItem.Sku.Equals("LA"))
                                singleItem.Img = "img_lamp_amber.png";
                            else if (singleItem.Sku.Equals("LB"))
                                singleItem.Img = "img_lamp_blue.png";
                            else if (singleItem.Sku.Equals("LG"))
                                singleItem.Img = "img_lamp_green.png";
                            else if (singleItem.Sku.Equals("LR"))
                                singleItem.Img = "img_lamp_red.png";
                            else if (singleItem.Sku.Equals("LW"))
                                singleItem.Img = "img_lamp_white.png";
                            else if (singleItem.Name.Equals("Clear Incandescent"))       //checked differently
                                singleItem.Img = "img_lamp_clear_incandescent.png";
                            else if (singleItem.Sku.Equals("F"))
                                singleItem.Img = "img_lamp_clear_flashing_incandescent.png";
                            else if (singleItem.Sku.Equals("NG"))
                                singleItem.Img = "img_lamp_neon_green.png";
                            else if (singleItem.Sku.Equals("NR"))
                                singleItem.Img = "img_lamp_neon_red.png";

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

                            //assigning images to items
                            //if (singleItem.Name.Contains("Aluminum"))
                            //    singleItem.Img = "img_clamp_ring_aluminum.png";
                            //else if (singleItem.Name.Contains("Polyester"))
                            //    singleItem.Img = "img_clamp_ring_polyester.png";

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

                            //assigning image to each item
                            //if (singleItem.Sku.Equals("IPBC"))
                            //    singleItem.Img = "";
                            //else if (singleItem.Sku.Equals("IPBCM"))
                            //    singleItem.Img = "";
                            //else if (singleItem.Sku.Equals("GIPBC"))
                            //    singleItem.Img = "";
                            //else if (singleItem.Sku.Equals("SIPBCM"))
                            //    singleItem.Img = "";

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

                            //assigning image to each item
                            if (singleItem.Sku.Equals("AR"))
                                singleItem.Img = "img_lens_amber.png";
                            else if (singleItem.Sku.Equals("BE"))
                                singleItem.Img = "img_lens_blue.png";
                            else if (singleItem.Sku.Equals("CR"))
                                singleItem.Img = "img_lens_clear.png";
                            else if (singleItem.Sku.Equals("GN"))
                                singleItem.Img = "img_lens_green.png";
                            else if (singleItem.Sku.Equals("RD"))
                                singleItem.Img = "img_lens_red.png";
                            else if (singleItem.Sku.Equals("WE"))
                                singleItem.Img = "img_lens_white.png";

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
                                //, Img = "img_ip20_guards.png"
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