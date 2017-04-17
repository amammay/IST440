using System.Collections.Generic;
using C3_Controls.Models.CouchDbConnections;

namespace C3_Controls.Models.DataStructuring
{

    /// <summary>
    /// @author Alex Mammay
    /// @updated 4/2/2017
    /// @email: amm7100@psu.edu
    /// This class acts a data structuring class for the Push To Test Items
    /// THis is the logic you use to specify any special use cases for UI representation of the items
    /// ie. specific images,etc.....
    /// </summary>
    public class PTT_Data
    {

        #region Private Fields

        public Dictionary<string, PTTItem[]> PttMap { get; }

        #endregion Private Fields

        #region Public Fields

        public List<PricedItem> Voltages { get; set; }
        public List<PricedItem> LampTypeColor { get; set; }
        public List<PricedItem> ClampRing { get; set; }
        public List<PricedItem> LensType { get; set; }
        public List<PricedItem> LensColor { get; set; }
        public PricedItem Options { get; set; }

        #endregion Public Fields

        #region Public Methods

        public PTT_Data()
        {
            
            //Instance of couch connector
            var myCouchDbConnector = new CouchDbConnector();
            
            //Creates our map
            PttMap = new Dictionary<string, PTTItem[]>();

            //Assign our dictionary to the one that was populated when the connection was made 
            PttMap = myCouchDbConnector.PttMap;
           
            //Fire off Method for doing the heavy lifting 
            PttDataStructure(PttMap);
           
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Actually structures of the data.
        /// </summary>
        /// <param name="pttMap"></param>
        public void PttDataStructure(Dictionary<string, PTTItem[]> pttMap)
        {
            //Initilaze the voltages
            Voltages = new List<PricedItem>();
           
            //Cycle over our wtl map 
            foreach (var valueSet in pttMap)
            {
                var valueSetItems = new List<PTTItem>();

                //Iterate over each value set, its going to be a ptt item 
                foreach (var valueSetItem in valueSet.Value)
                    valueSetItems.Add(valueSetItem);

                //Switch on the value set key
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
                            else if (singleItem.Sku.Equals("NL"))
                                singleItem.Img = "img_voltages.png";
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

                            
                            if (singleItem.Name.Contains("Aluminum"))
                                singleItem.Img = "AluminumClampRing.jpg";
                            else if (singleItem.Name.Contains("Polyester"))
                                singleItem.Img = "img_clamp_ring_polyester.png";

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
                            if (singleItem.Sku.Equals("IPBC"))
                                singleItem.Img = "illuminated.jpg";
                            else if (singleItem.Sku.Equals("IPBCM"))
                                singleItem.Img = "illuminated_mushroom.jpg";
                            else if (singleItem.Sku.Equals("GIPBC"))
                                singleItem.Img = "illuminated_guarded.jpg";
                            else if (singleItem.Sku.Equals("SIPBCM"))
                                singleItem.Img = "shrouded_illuminated_push_button.jpg";

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

        #endregion Private Methods



    }
}