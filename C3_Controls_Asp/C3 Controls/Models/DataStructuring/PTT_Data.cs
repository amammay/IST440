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
            PttMap = new Dictionary<string, DataItem[]>();

            //Assign our dictionary to the one that was populated when the connection was made 
            PttMap = myCouchDbConnector.PttMap;

            //Fire off Method for doing the heavy lifting 
            PttDataStructure(PttMap);
        }

        #endregion Public Methods

        #region Private Fields

        public Dictionary<string, DataItem[]> PttMap { get; }

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        ///     Actually structures of the data.
        /// </summary>
        /// <param name="pttMap"></param>
        public void PttDataStructure(Dictionary<string, DataItem[]> pttMap)
        {
            //Cycle over our ptt map 
            foreach (var valueSet in pttMap)
            {
                var valueSetItems = new List<DataItem>();

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

                            if (singleItem.Sku.Equals("6"))
                                singleItem.Img = "6vacdc.png";
                            else if (singleItem.Sku.Equals("12"))
                                singleItem.Img = "12vacdc.png";
                            else if (singleItem.Sku.Equals("24"))
                                singleItem.Img = "24acdc.png";
                            else if (singleItem.Sku.Equals("120"))
                                singleItem.Img = "120vacdc.png";
                            else if (singleItem.Sku.Equals("240"))
                                singleItem.Img = "120vac.png";
                            else if (singleItem.Sku.Equals("277"))
                                singleItem.Img = "120vac.png";
                           
                            else if (singleItem.Sku.Equals("480"))
                                singleItem.Img = "480vac.png";



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
                                Img = "img_voltages.png"
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
                            if (singleItem.Sku.Equals("A"))
                                singleItem.Img = "img_lamp_amber.png";
                            else if (singleItem.Sku.Equals("B"))
                                singleItem.Img = "img_lamp_blue.png";
                            else if (singleItem.Sku.Equals("G"))
                                singleItem.Img = "img_lamp_green.png";
                            else if (singleItem.Sku.Equals("R"))
                                singleItem.Img = "img_lamp_red.png";
                            else if (singleItem.Sku.Equals("W"))
                                singleItem.Img = "img_lamp_white.png";
                            else if (singleItem.Sku.Equals("")) //checked differently
                                singleItem.Img = "img_lamp_clear_incandescent.png";
                            else if (singleItem.Sku.Equals("F"))
                                singleItem.Img = "img_lamp_clear_flashing_incandescent.png";
                            else if (singleItem.Sku.Equals("NG"))
                                singleItem.Img = "img_lamp_neon_green.png";
                            else if (singleItem.Sku.Equals("NR"))
                                singleItem.Img = "img_lamp_neon_red.png";
                            else if (singleItem.Name.Equals("No Lamp"))
                                singleItem.Img = "Nothing.png";
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
                                singleItem.Img = "AluminumClampRing.png";
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
                            if (singleItem.Sku.Equals("IPBC"))
                                singleItem.Img = "illuminated.png";
                            else if (singleItem.Sku.Equals("IPBCM"))
                                singleItem.Img = "illuminated_mushroom.png";
                            else if (singleItem.Sku.Equals("GIPBC"))
                                singleItem.Img = "illuminated_guarded.png";
                            else if (singleItem.Sku.Equals("SIPBCM"))
                                singleItem.Img = "shrouded_illuminated_push_button.png";

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
                            else if (singleItem.Sku.Equals("YW"))
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

                            if (singleItem.Sku.Equals("IP20"))
                                singleItem.Img = "ip20guard.png";
                            if (singleItem.Sku.Equals(""))
                                singleItem.Img = "Nothing.png";

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