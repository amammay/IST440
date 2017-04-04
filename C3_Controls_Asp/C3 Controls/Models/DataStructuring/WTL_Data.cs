using System;
using System.Collections.Generic;
using System.Web.Configuration;
using C3_Controls.Models.CouchDbConnections;

namespace C3_Controls.Models.DataStructuring
{

    /// <summary>
    /// @author Alex Mammay
    /// @updated 4/2/2017
    /// @email: amm7100@psu.edu
    /// This class acts a data structuring class
    /// </summary>
    public class WTL_Data
    {

        #region Private Fields

        public Dictionary<string, WTLItem[]> WtlMap { get; }

        #endregion Private Fields

        #region Public Fields

        public PricedItem OperatorType { get; set; }
        public PricedItem ModuelDiameter { get; set; }
        public List<PricedItem> Voltages { get; set; }
        public List<PricedItem> Styles { get; set; }
        public List<PricedItem> Positions { get; set; }
        public List<PricedItem> SoundModule { get; set; }
        public List<PricedItem> LightLens { get; set; }

        #endregion Public Fields
        
        #region Public Methods

        /// <summary>
        /// This is were dictionary specific values get their proper inheritence of a priced item
        /// </summary>
        public WTL_Data()
        {
        
            //Instance of couch connector
            var myCouchDbConnector = new CouchDbConnector();

            //Creates our map
            WtlMap = new Dictionary<string, WTLItem[]>();

            //Assign our dictionary to the one that was populated when the connection was made 
            WtlMap = myCouchDbConnector.WtlMap;
            
            //Fire off Method for doing the heavy lifting 
            WtlDataStructure(WtlMap);
           
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Actually structures of the data.
        /// </summary>
        /// <param name="wtlMap"></param>
        public void WtlDataStructure(Dictionary<string, WTLItem[]> wtlMap)
        {
            //Cycle over our wtl map 
            foreach (var valueSet in wtlMap)
            {
                var valueSetItems = new List<WTLItem>();

                //Iterate over each value set, its going to be a wtl item 
                foreach (var valueSetItem in valueSet.Value)
                    valueSetItems.Add(valueSetItem);

                //Switch on the value set key
                switch (valueSet.Key)
                {
                    //Handles operator types
                    case "Operator Type":
                        foreach (var item in valueSetItems)
                            OperatorType = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Sku = item.sku,
                                Desc = item.text
                            };
                        break;

                    case "Diameter":
                        foreach (var item in valueSetItems)
                            ModuelDiameter = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Sku = item.sku,
                                Desc = item.text
                            };
                        break;

                    case "Base Material & Style":
                        Styles = new List<PricedItem>();
                        foreach (var item in valueSetItems)
                        {
                            var singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };

                            //extra logic to assign an image to the item
                            switch (singleItem.Sku)
                            {
                                case "P1":
                                    singleItem.Img = "img_base_short.png";
                                    break;
                                case "P2":
                                    singleItem.Img = "img_base_long.png";
                                    break;
                                case "P3":
                                    singleItem.Img = "img_base_short.png";
                                    break;
                            }

                            //Add it to the list
                            Styles.Add(singleItem);
                        }
                        break;

                    case "Voltage":
                        Voltages = new List<PricedItem>();
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

                    case "Light Lens":
                        LightLens = new List<PricedItem>();
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
                            LightLens.Add(singleItem);
                        }
                        break;

                    case "Sound Module":
                        SoundModule = new List<PricedItem>();
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
                            SoundModule.Add(singleItem);
                        }
                        break;

                    case "Position":
                        Positions = new List<PricedItem>();
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
                            if (singleItem.Sku.EndsWith("A"))
                                singleItem.Img = "img_light_amber.png";
                            else if (singleItem.Sku.EndsWith("B"))
                                singleItem.Img = "img_light_blue.png";
                            else if (singleItem.Sku.EndsWith("G"))
                                singleItem.Img = "img_light_green.png";
                            else if (singleItem.Sku.EndsWith("R"))
                                singleItem.Img = "img_light_red.png";
                            else if (singleItem.Sku.EndsWith("W"))
                                singleItem.Img = "img_light_white.png";
                            //Add it to the list
                            Positions.Add(singleItem);
                        }
                        break;
                    default:
                        Console.WriteLine("Error, Cant Strucutre data");
                        break;
                }
            }
        }

        #endregion Private Methods


    }
}