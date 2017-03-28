using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace C3_Controls.Models.DataStructuring
{
    public class WTL_Data
    {
        public Dictionary<string, WTLItem[]> WtlMap { get; set; }
        //public Dictionary<string, PTTItem[]> PttMap { get; set; }
        public string CmSetting => WebConfigurationManager.AppSettings["CurrentDatebase"];


        public PricedItem OperatorType { get; set; }
        public PricedItem ModuelDiameter { get; set; }
        public List<PricedItem> Voltages { get; set; }
        public List<PricedItem> Styles { get; set; }
        public List<PricedItem> Positions { get; set; }
        public List<PricedItem> SoundModule { get; set; }
        public List<PricedItem> LightLens { get; set; }

        public WTL_Data()
        {
            if (CmSetting != "internal")
            {
                CouchDbConnector myCouchDbConnector = new CouchDbConnector();

                WtlMap = new Dictionary<string, WTLItem[]>();

                //Assign our dictionary to the one that was populated when the connection was made 
                WtlMap = myCouchDbConnector.WtlMap;
            }

            //Creats a list of all of our keys in the dictionary
            List<string> WtlKeyMap = new List<string>();

            //Iterate over items in our map and add them to key list
            foreach (var key in WtlMap.Keys)
            {

                WtlKeyMap.Add(key);

            }

            //Cycle over our wtl map 
            foreach (var valueSet in WtlMap)
            {
                List<WTLItem> valueSetItems = new List<WTLItem>();

                //TODO document
                foreach (var valueSetItem in valueSet.Value)
                {
                    valueSetItems.Add(valueSetItem);
                }

                switch (valueSet.Key)
                {
                    case "Operator Type":
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            OperatorType = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Sku = item.sku,
                                Desc = item.text
                                
                            };

                        }
                        break;

                    case "Diameter":
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            ModuelDiameter = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Sku = item.sku,
                                Desc = item.text

                            };

                        }
                        break;

                    case "Base Material & Style":
                        Styles = new List<PricedItem>();
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            PricedItem singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            Styles.Add(singleItem);
                        }
                        break;

                    case "Voltage":
                        Voltages = new List<PricedItem>();
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            PricedItem singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            Voltages.Add(singleItem);
                        }
                        break;

                    case "Light Lens":
                        LightLens = new List<PricedItem>();
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            PricedItem singleItem = new PricedItem
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
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            PricedItem singleItem = new PricedItem
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
                        //TODO document
                        foreach (var item in valueSetItems)
                        {
                            PricedItem singleItem = new PricedItem
                            {
                                Name = item.text,
                                Price = item.price,
                                Desc = item.text,
                                Sku = item.sku
                            };
                            //Add her to the list
                            Positions.Add(singleItem);
                        }
                        break;

                }


            }
        }

        
       

    }
}