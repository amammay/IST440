using System.Collections.Generic;
using C3_Controls.Models.CouchDbConnections;

namespace C3_Controls.Models.DataStructuring
{
    /// <summary>
    ///     @author Alex Mammay
    ///     @updated 4/2/2017
    ///     @email: amm7100@psu.edu
    ///     This class acts a data structuring class for the Push To Test Items
    ///     THis is the logic you use to specify any special use cases for UI representation of the items
    ///     ie. specific images,etc.....
    /// </summary>
    public class PTT_Data
    {
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

        #region Private Fields

        public Dictionary<string, PTTItem[]> PttMap { get; }

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        ///     Actually structures of the data.
        /// </summary>
        /// <param name="pttMap"></param>
        public void PttDataStructure(Dictionary<string, PTTItem[]> pttMap)
        {
            //Cycle over our ptt map 
            foreach (var valueSet in pttMap)
            {
                var valueSetItems = new List<PTTItem>();

                //Iterate over each value set, its going to be a ptt item 
                foreach (var valueSetItem in valueSet.Value)
                    valueSetItems.Add(valueSetItem);

                //Switch on the value set key
                switch (valueSet.Key)
                {
                    case "Voltages":
                        Voltages = new List<PricedItem>();
                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };

                            if (singleItem.Sku.Equals("6V"))
                                singleItem.Img = "6vacdc.png";
                            else if (singleItem.Sku.Equals("12V"))
                                singleItem.Img = "12vacdc.png";
                            else if (singleItem.Sku.Equals("24V"))
                                singleItem.Img = "24acdc.png";
                            else if (singleItem.Sku.Equals("120V"))
                                singleItem.Img = "120vacdc.png";

                            else if (singleItem.Sku.Equals("120V_AC"))
                                singleItem.Img = "120vacdc.png";
                            else if (singleItem.Sku.Equals("240V_AC"))
                                singleItem.Img = "120vac.png";
                            else if (singleItem.Sku.Equals("277V_AC"))
                                singleItem.Img = "120vac.png";
                            
                            //to change
                            else if (singleItem.Sku.Equals("480V_AC"))
                                singleItem.Img = "120vac.png";

                            else if (singleItem.Sku.Equals("240V"))
                                singleItem.Img = "120vac.png";
                            //to change
                            else if (singleItem.Sku.Equals("480V"))
                                singleItem.Img = "120vac.png";




                            //Add her to the list
                            Voltages.Add(singleItem);
                        }
                        break;

                    case "BasicOperators":
                        BasicOperators = new List<PricedItem>();
                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku,
                                Img = "img_push_to_test.png"
                            };
                            //Add her to the list
                            BasicOperators.Add(singleItem);
                        }
                        break;

                    case "LampColors":
                        LampColors = new List<PricedItem>();
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
                            if (singleItem.Sku.Contains("_A"))
                                singleItem.Img = "img_lamp_amber.png";
                            else if (singleItem.Sku.Contains("_B"))
                                singleItem.Img = "img_lamp_blue.png";
                            else if (singleItem.Sku.Contains("_G"))
                                singleItem.Img = "img_lamp_green.png";
                            else if (singleItem.Sku.Contains("_R"))
                                singleItem.Img = "img_lamp_red.png";
                            else if (singleItem.Sku.Contains("_W"))
                                singleItem.Img = "img_lamp_white.png";
                            else if (singleItem.Sku.Contains("_CI")) //checked differently
                                singleItem.Img = "img_lamp_clear_incandescent.png";
                            else if (singleItem.Sku.Contains("_CLI"))
                                singleItem.Img = "img_lamp_clear_flashing_incandescent.png";
                            else if (singleItem.Sku.Contains("_NG"))
                                singleItem.Img = "img_lamp_neon_green.png";
                            else if (singleItem.Sku.Contains("_NR"))
                                singleItem.Img = "img_lamp_neon_red.png";
                            else if (singleItem.Name.Equals("No Lamp"))
                                singleItem.Img = "img_voltages.png";
                            //Add her to the list
                            LampColors.Add(singleItem);
                        }
                        break;
                    case "ClampRings":
                        ClampRings = new List<PricedItem>();
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
                            ClampRings.Add(singleItem);
                        }
                        break;
                    case "LensTypes":
                        LensTypes = new List<PricedItem>();
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
                            if (singleItem.Sku.Equals("IPBL"))
                                singleItem.Img = "illuminated.jpg";
                            else if (singleItem.Sku.Equals("IPBML"))
                                singleItem.Img = "illuminated_mushroom.jpg";
                            else if (singleItem.Sku.Equals("GIPBL"))
                                singleItem.Img = "illuminated_guarded.jpg";
                            else if (singleItem.Sku.Equals("SIPBML"))
                                singleItem.Img = "shrouded_illuminated_push_button.jpg";

                            //Add her to the list
                            LensTypes.Add(singleItem);
                        }
                        break;
                    case "LensColors":
                        LensColors = new List<PricedItem>();
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
                            if (singleItem.Sku.Equals("ALE"))
                                singleItem.Img = "img_lens_amber.png";
                            else if (singleItem.Sku.Equals("BLE"))
                                singleItem.Img = "img_lens_blue.png";
                            else if (singleItem.Sku.Equals("CLE"))
                                singleItem.Img = "img_lens_clear.png";
                            else if (singleItem.Sku.Equals("GLE"))
                                singleItem.Img = "img_lens_green.png";
                            else if (singleItem.Sku.Equals("RLE"))
                                singleItem.Img = "img_lens_red.png";
                            else if (singleItem.Sku.Equals("WLE"))
                                singleItem.Img = "img_lens_white.png";
                            else if (singleItem.Sku.Equals("YLE"))
                                singleItem.Img = "img_lens_yellow.png";

                            //Add her to the list
                            LensColors.Add(singleItem);
                        }
                        break;

                    case "Options":
                        Options = new List<PricedItem>();
                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };

                            if (singleItem.Sku.Equals("IPG"))
                                singleItem.Img = "search.gif";

                            Options.Add(singleItem);
                        }

                        break;
                }
            }
        }

        #endregion Private Methods

        #region Public Fields

        public List<PricedItem> OperatorType { get; set; }
        public List<PricedItem> Voltages { get; set; }
        public List<PricedItem> BasicOperators { get; set; }
        public List<PricedItem> LampColors { get; set; }
        public List<PricedItem> ClampRings { get; set; }
        public List<PricedItem> LensTypes { get; set; }
        public List<PricedItem> LensColors { get; set; }
        public List<PricedItem> Options { get; set; }

        #endregion Public Fields
    }
}