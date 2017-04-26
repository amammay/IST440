using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<PricedItem> PositionsClearlens { get; set; }

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
                                    singleItem.Img = "img_base_direct.png";
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
                            };

                            switch (singleItem.Sku)
                            {
                                case "MC":
                                    singleItem.Img = "24acdc.png";
                                    break;
                                case "D":
                                    singleItem.Img = "120vacdc.png";
                                    break;
                                case "F":
                                    singleItem.Img = "120vac.png";
                                    break;
                                default:
                                    break;

                            }

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
                                Sku = item.sku,
                                Img = "sound.png"
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
                            {
                                switch (singleItem.Sku)
                                {
                                    case "DA":
                                        singleItem.Img = "img_light_amber.png";
                                        break;
                                    case "FA":
                                        singleItem.Img = "AmberFlashingTowerLight.png";
                                        break;
                                    case "RA":
                                        singleItem.Img = "AmberRotaryTowerLight.png";
                                        break;

                                }
                            }
                              
                            else if (singleItem.Sku.EndsWith("B"))
                            {
                                switch (singleItem.Sku)
                                {
                                    case "DB":
                                        singleItem.Img = "img_light_blue.png";
                                        break;
                                    case "FB":
                                        singleItem.Img = "BlueFlashingTowerLight.png";
                                        break;
                                    case "RB":
                                        singleItem.Img = "BlueRotaryTowerLight.png";
                                        break;

                                }
                            }
                            else if (singleItem.Sku.EndsWith("G"))
                            {
                                switch (singleItem.Sku)
                                {
                                    case "DG":
                                        singleItem.Img = "img_light_green.png";
                                        break;
                                    case "FG":
                                        singleItem.Img = "GreenFlashingTowerLight.png";
                                        break;
                                    case "RG":
                                        singleItem.Img = "GreenRotaryTowerLight.png";
                                        break;

                                }
                            }
                              
                            else if (singleItem.Sku.EndsWith("R"))
                            {
                                switch (singleItem.Sku)
                                {
                                    case "DR":
                                        singleItem.Img = "img_light_red.png";
                                        break;
                                    case "FR":
                                        singleItem.Img = "RedFlashingTowerLight.png";
                                        break;
                                    case "RR":
                                        singleItem.Img = "RedRotaryTowerLight.png";
                                        break;

                                }
                            }
                             
                            else if (singleItem.Sku.EndsWith("W"))
                            {
                                switch (singleItem.Sku)
                                {
                                    case "DW":
                                        singleItem.Img = "img_light_white.png";
                                        break;
                                    case "FW":
                                        singleItem.Img = "WhiteFlashingTowerLight.png";
                                        break;
                                    case "RW":
                                        singleItem.Img = "WhiteRotaryTowerLight.png";
                                        break;

                                }
                            }


                                
                            //Add it to the list
                            Positions.Add(singleItem);
                        }
                        break;
                    default:
                        Console.WriteLine("Error, Cant Strucutre data");
                        break;
                }
            }

            PositionsClearlens = new List<PricedItem>();
            foreach (var positionItem in Positions)
            {
                var tempPositionItem = new PricedItem();
                

                var tempSku = positionItem.Sku;
                var tempImg = positionItem.Img;
                var tempName = positionItem.Name;

                if (positionItem.Sku.EndsWith("A") || positionItem.Sku.EndsWith("B")
                    || positionItem.Sku.EndsWith("G") || positionItem.Sku.EndsWith("R"))
                {
                    tempPositionItem.Sku = tempSku.Insert(1, "C");
                    tempPositionItem.Name = tempName + "Clear Lens";
                    tempPositionItem.Desc = tempName + "Clear Lens";
                    tempPositionItem.Img = tempImg.Replace(".png", "_clear.png");

                    if (tempPositionItem.Sku.StartsWith("D"))
                    {
                        tempPositionItem.Price = 48.50;
                    }
                    else
                    {
                        tempPositionItem.Price = 61.00;
                    }

                    PositionsClearlens.Add(tempPositionItem);
                }

              
            }

            
            foreach (var item in PositionsClearlens)
            {
                Positions.Add(item);
            }


        }

        #endregion Private Methods
    }
}